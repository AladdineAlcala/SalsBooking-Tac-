var $tablePayments = null;

$(document).ready(function() {


    var row = $('#tbl_paymentBooking tbody >tr');

    var data = $("#tbl_paymentBooking tr:eq(1)");

    // var x = parseFloat(data.find("td:eq(5)").text().replace(/,/g, '')).toFixed(2);
    var init_bal = cleanDecimal(data.find("td:eq(5)").text());

    var temp;
    var totaldebit = 0;
    var balance = 0;

    for (var i = 1; i < row.length; i++) {

        //  var val = $(row[i].cells[4]).text();

        
        var r = $(row[i]);
        var prevrow = r.prev();

        //var credit_val = cleanDecimal($(row[i].cells[3]).text());
        var debit_val = cleanDecimal($(row[i].cells[3]).text());

        //console.log("debit " + val);

        if (debit_val > 0) {

            temp = init_bal - debit_val;
            balance = temp;
            totaldebit = parseFloat(totaldebit) + parseFloat(debit_val);
            
           // console.log(totaldebit);
        }

        totaldebit = parseFloat(totaldebit);

       // var prevBal = prevrow.find("td:eq(5)").text();
       // init_bal = cleanDecimal(prevBal);
        init_bal = balance;
    

        if (isNaN(parseFloat(balance))) {
            balance = 0;

        } else {

            //balance = balance.toFixed(2);

            $(row[i].cells[5]).text(currencyFormat(balance));
        }

       
    }

    $('#totalPayment').html(currencyFormat(totaldebit));

  
    $('#endbalance').html(currencyFormat(balance));



    function cleanDecimal(val) {

        return parseFloat(val.replace(/,/g, '')).toFixed(2);
    }

    function currencyFormat(num) {
        return num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
    }

  

}); //======= end of code document.ready====================


//--------- load table payments--------------------------
function loadTablePayments(tranId) {

    if ($.fn.DataTable.isDataTable('#tbl_paymentBooking')) {

        $('#tbl_paymentBooking').dataTable().fnDestroy();
        $('#tbl_paymentBooking').dataTable().empty();

    }

    var id = tranId;
    var total_Payment = 0;

    $tablePayments = $('#tbl_paymentBooking').DataTable({
        "serverSide": false,
        "dom": "<'#payment_top.row'<'col-sm-6'B><'col-sm-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'#payment_bottom.row'<'col-sm-12 '>>",


        "columnDefs": [
            { "orderable": false, "targets": 5 },
            { "width": "150px", "targets": 5 },
            { "width": "100px", "targets": 6 }
        ],

        buttons:
        [
            {
                text: '<i class="fa fa-plus-square fa-sm fa-fw"></i>',
                className: 'btn btn-primary btn-sm btnAddPayment',
                titleAttr: 'Create new payment',
                action: function () {

                   // onAddNewPayment(id);
                }


            },
            {
                text: '<i class="fa fa-print fa-sm fa-fw"></i>',
                className: 'btn btn-primary btn-sm btnPrintPayment',
                titleAttr: 'Print',
                action: function () {

                    //onAddNewPayment(id);
                }


            }
        ]


    });
}


$(document).on('click', '#btn_regPayment', function (e) {
    e.preventDefault();

    swal({
        title: "Are You Sure ?",
        text: "Confirm Adding Payment..",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Save it!',
        closeOnConfirm: true,
        closeOnCancel: true

    },
        function (isConfirm) {

            if (isConfirm) {

                var formUrl = $('#paymentform').attr('action');
                var form = $('[id*=paymentform]');
              
                $.validator.unobtrusive.parse(form);
                form.validate();


                if (form.valid()) {
                  

                    $('#spinn-loader').show();
                    $.ajax({
                        type: 'POST',
                        url: formUrl,
                        data: form.serialize(),
                        datatype: 'json',
                        cache: false,
                        success: function (data) {

                            if (data.success) {
                               
                                //console.log(data.url);
                                swal({
                                    title: "Success",
                                    text: "It was succesfully Added!",
                                    type: "success"
                                      },
                                    function () {

                                      
                                       
                                    });

                                $('#paymentsTable').load(data.url);
                             
                                setTimeout(function () {
                                   
                                    $("#modal-PaymentBooking").modal('hide');
                                    $('#spinn-loader').hide();
                                }, 500);
                                
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            swal("Error adding record!", "Please try again", "error");

                            $('#spinn-loader').hide();
                        }
                    });

                }
                else {

                    $('#spinn-loader').hide();

                    $.each(form.validate().errorList, function (key, value) {
                        $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                        $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                   
                }
            }
        }
    );


});

$(document).on('click',
    '#btn_cancelpaymnt',
    function (e) {
        e.preventDefault();

        setTimeout(function () { $("#modal-PaymentBooking").modal('hide') }, 300);
    });

$(document).on('click',
    '#btn_cancelupdatePaymnt',
    function (e) {
        e.preventDefault();

        setTimeout(function () { $("#modal-PaymentBooking").modal('hide') }, 300);
    });



//remove payment
$(document).on('click', '.removepayment', function (e) {
    e.preventDefault();

    var _pmtId = $(this).attr("id");

    swal({
        title: "Are You Sure ?",
        text: "Confirm Removing This Payment..",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Remove it!',
        closeOnConfirm: true,
        closeOnCancel: true

    },
        function (isConfirm) {

            if (isConfirm) {
                 $('#spinn-loader').show();
                $.ajax({

                    type: "post",
                    url: paymentsUrl.payUrl_removePayment,
                    ajaxasync: true,
                    data: { pmtNo: _pmtId },
                    cache: false,
                    success: function (data) {

                        if (data.success) {

                            swal({
                                title: "Success",
                                text: "It was succesfully removed!",
                                type: "success"
                            },
                                function () {

                                    
                                });


                            setTimeout(function () {

                                $('#paymentsTable').load(data.url);
                                $('#spinn-loader').hide();
                            }, 500);

                           

                            $('#spinn-loader').hide();
                        }
                    }

                });
                //ajax end
            }
        }
    );
});

// update payment

$(document).on('click', '.updatepayment', function (e) {

    var pymtId = $(this).attr('id');

    UpdatePayment(pymtId);

});

function UpdatePayment(pymtId) {
    var id = pymtId;

    $.ajax({

        type: 'Get',
        url: paymentsUrl.payUrl_updatePayment,
        contentType: 'application/html;charset=utf8',
        data: { pymtId: id },
        datatype: 'html',
        cache: false,
        success: function (result) {
            var modal = $('#modal-PaymentBooking');
            modal.find('#modalcontent').html(result);

            modal.modal({
                backdrop: 'static',
                keyboard: false
            },
                'show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Error updating record!", "Please try again", "error");
        }



    });
}

//update payment post

$(document).on('click', '#btn_updatePayment', function (e) {
    e.preventDefault();

    swal({
        title: "Are You Sure ?",
        text: "Confirm Updating Payment..",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        //confirmButtonText: 'Yes, Save it!',
        closeOnConfirm: true,
        closeOnCancel: true

    },
        function (isConfirm) {

            if (isConfirm) {
                $('#spinn-loader').show();

                var formUrl = $('#Updatepaymentform').attr('action');
                var form = $('[id*=Updatepaymentform]');
                $.validator.unobtrusive.parse(form);
                form.validate();

                if (form.valid()) {

                    $.ajax({
                        type: 'POST',
                        url: formUrl,
                        data: form.serialize(),
                        datatype: 'json',
                        cache: false,
                        success: function (data) {
                            if (data.success) {

                                swal({
                                    title: "Success",
                                    text: "It was succesfully Updated!",
                                    type: "success"
                                },
                                    function () {

                                      

                                    });
                                $('#paymentsTable').load(data.url);
                                setTimeout(function () {
                                    $("#modal-PaymentBooking").modal('hide');

                                    $('#spinn-loader').hide();
                                }, 500);

                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            swal("Error updating record!", "Please try again", "error");
                        }
                    });

                }
                else {

                    $.each(form.validate().errorList, function (key, value) {
                        $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                        $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                        $errorSpan.show();
                    });

                }
            }
        }
    );


});

//function onAddNewPayment(id) {

$(document).on('click','#add_payment',function(e) {
        e.preventDefault();
     
        $.ajax({
            type: 'Get',
            url: paymentsUrl.payUrl_addPayment,
            // url:'http://localhost:Sals/Payments/Add_PaymentPartialView',
            contentType: 'application/html;charset=utf8',
            data: { transactionId:  $(this).data('id')  },
            datatype: 'html',
            cache: false,
            success: function(result) {
                var modal = $('#modal-PaymentBooking');
                modal.find('#modalcontent').html(result);

                modal.modal({
                        backdrop: 'static',
                        keyboard: false
                    },
                    'show');
            },
            error: function(xhr, ajaxOptions, thrownError) {
                swal("Error adding record!", "Please try again", "error");
            }


        });
    });