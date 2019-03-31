
var $selectedObject;
var $tablebookings;
var $areasel;

$(document).ready(function () {



   // var onrefreshBooking;

    $('#packageselect').select2({

        dropdownParent: $('#modal-searchPackage')

    });

 

    //Custom date validation overide for date formats
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD-MMM-YYYY HH:mm A", true).isValid();
    }

    ////init datetimepcker
    //$('#event_date').datetimepicker(
    //    {
    //        //format: "DD-MMM-YYYY",
    //        //defaultDate: dateNow

    //        //maxDate: moment(),
    //        allowInputToggle: true,
    //        enabledHours: false,
    //        locale: moment().local('en'),
    //        format: "DD-MMM-YYYY HH:mm A",
    //        defaultDate: dateNow

    //    }
    //    );




    $.fn.dataTable.moment('MMM-DD-YYYY hh:mm');


    loaddatatableBookings();


    //  $('#tbl_eventsBooking_filter input').addClass('form-control');

    $('#btn_regbooking').on('click', function (e) {

        e.preventDefault();


        swal({
            title: "Are You Sure ?",
            text: "Confirm Saving Booking Details..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            closeOnConfirm: true, closeOnCancel: true

        },
            function (isConfirm) {
                if (isConfirm) {

                    var formUrl = $('#savebooking').attr('action');

                    var form = $('[id*=savebooking]');

                    //console.log(form);

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
                                        text: "It was succesfully added!",
                                        type: "success"
                                    },
                                    function() {

                                     


                                    });

                                    //window.location.href = bookingsUrl.bookUrl_IndexLoad;
                                    setTimeout(function () {

                                        window.location.href = bookingsUrl.bookUrl_getPackageBookingDetailsId.replace("trans_Id", data.trnsId);

                                        // $('#spinn-loader').hide();
                                    }, 1000);

                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Error adding record!", "Please try again", "error");
                            }
                        });

                    } else {
                        $.each(form.validate().errorList, function (key, value) {
                            $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                            $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                            $errorSpan.show();
                        });
                    }

                    //  showlastrecord(menulist);
                }
            });



    });


    $.fn.dataTable.moment = function (format, locale) {

        var types = $.fn.dataTable.ext.type;

        types.detect.unshift(function (d) {
            return moment(d, format, locale, true).isValid() ? 'moment-' + format : null;
        });

        types.order['moment-' + format + '-pre'] = function (d) {
            return moment(d, format, locale, true).unix();
        };
    }



    $('#modalSearchPackage').on('click', function (e) {
        e.preventDefault();



        $.ajax({
            type: 'Get',
            url: bookingsUrl.bookUrl_searchPackage,
            contentType: 'application/html;charset=utf8',
            //data: { packageId: $(this).data('id') },
            datatype: 'html',
            cache: false,
            success: function (result) {
                var modal = $('#modal-searchPackage');

                modal.find('#modalcontent').html(result);

                var tableSearchPackage = $('#tbl-packages').DataTable({ bLengthChange: false, bFilter: false });


                tableSearchPackage.columns.adjust();

                //tableSearchPackage.destroy();

                modal.modal({
                    backdrop: 'static',
                    keyboard: false
                }, 'show');

            }, error: function (xhr, ajaxOptions, thrownError) {
                swal("Error adding record!", "Please try again", "error");
            }



        });



    });


    //cancel booking click event

    $('#btn_cancelbooking').on('click',
        function (e) {

            e.preventDefault();

            swal({
                title: "Are You Sure ?",
                text: "Confirm Cancel Booking ",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Cancel This Booking!',
                closeOnConfirm: true, closeOnCancel: true

            },
                function (isConfirm) {
                    if (isConfirm) {

                        window.history.back();

                    }

                });

        });


    $('#btn_cancelupdatebooking').on('click',
        function (e) {

            e.preventDefault();

            swal({
                title: "Are You Sure ?",
                text: "Confirm Cancel Updating this Booking ",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Cancel This Booking!',
                closeOnConfirm: true, closeOnCancel: true

            },
                function (isConfirm) {
                    if (isConfirm) {

                        window.history.back();

                    }

                });

        });


    //table select row click event -- remove/edit record



    $('#tbl_eventsBooking tbody').on('click', 'tr', function (e) {

        e.stopPropagation();

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');

            $tablebookings.button(0).enable();
            $tablebookings.button(1).disable();
            $tablebookings.button(2).disable();
            $tablebookings.button(3).disable();
            $tablebookings.button(4).disable();


        } else {

            $tablebookings.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

            $tablebookings.button(0).disable();
            $tablebookings.button(1).enable();
            $tablebookings.button(2).enable();
            $tablebookings.button(3).enable();
            $tablebookings.button(4).enable();

            $selectedObject = $(this);

        }
    });


    $('#tbl_eventsBooking tbody').on('click', 'tr .getdetails',
        function (e) {

            e.preventDefault();


            var trnsId = $(this).attr("id");

            $.ajax({
                type: 'Get',
                url: bookingsUrl.bookUrl_getPackageId,
                data: { transactionId: trnsId },
                success: function (result) {
                    if (result.success) {
                        window.location.href = bookingsUrl.bookUrl_getPackageBookingDetailsId.replace("trans_Id", trnsId);
                    }
                    else {
                        alert('No Package Available on this transaction');
                    }
                }
            });


        });


    $('#tbl_eventsBooking tbody').on('click',
        'tr .getpackage',
        function (e) {

            e.preventDefault();

            var _transid = ($(this).attr('id'));
            // console.log(_transid);

            $.ajax({
                type: 'GET',
                url: bookingsUrl.bookUrl_getPackageId,
                data: { transactionId: $(this).attr('id') },
                success: function (result) {
                    if (result.success) {
                        window.location.href = bookingsUrl.bookUrl_packageBooking.replace("trans_Id", _transid);
                    } else {
                        alert('No Package Available on this transaction');
                    }
                }
            });
        });


    $('#tbl_eventsBooking tbody').on('click','tr .getpayments',function(e) {

        e.preventDefault();

        var _transid = ($(this).attr('id'));

        $.ajax({
            type: 'Get',
            url:  bookingsUrl.bookUrl_getPackageId,
            data: { transactionId: $(this).attr('id') },
            success:function(result) {
                if (result.success) {
                    window.location.href = bookingsUrl.bookUrl_paymentBooking.replace("tId", _transid);

                } else {
                    alert('No Package Available on this transaction');
                }
            }
        });

    });



    var onEditBooking = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var booktransactionId = $this.attr('data-transid');

            window.location.href = bookingsUrl.bookUrl_editBooking.replace("tId", booktransactionId);


        }
    };

    //=============  update booking command  ===================

    $('#btn_modifybooking').on('click', function (e) {

        e.preventDefault();


        swal({
            title: "Are You Sure ?",
            text: "Confirm Update Booking Details..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            closeOnConfirm: true, closeOnCancel: true

        },
            function (isConfirm) {
                if (isConfirm) {

                    var formUrl = $('#modifybooking').attr('action');

                    var form = $('[id*=modifybooking]');

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
                                        text: "It was succesfully updated!",
                                        type: "success"
                                    },
                                        function () {

                                          

                                           
                                        });

                                    setTimeout(function () {

                                        //window.location.href = PackageUrl.url_details.replace("pId", data.packageId);
                                        window.location.href = bookingsUrl.bookUrl_IndexLoad;

                                        // $('#spinn-loader').hide();
                                    }, 1000);
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Error adding record!", "Please try again", "error");
                            }
                        });

                    } else {
                        $.each(form.validate().errorList, function (key, value) {
                            $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                            $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                            $errorSpan.show();
                        });
                    }

                    //  showlastrecord(menulist);
                }
            });



    });


    //================= end of code for update booking ====================================




    // Date Created: 1-11-2019
    //================ remove booking command===============================================



    var onremoveBooking = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var booktransactionId = $this.attr('data-transid');

            swal({
                title: "Are You Sure ?",
                text: "Confirm Removing This Booking..",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Remove it!',
                closeOnConfirm: true,
                closeOnCancel: true

            }, function (isConfirm) {

                if (isConfirm) {

                    $.ajax({
                        type: "post",
                        url: bookingsUrl.bookUrl_removeBooking,
                        ajaxasync: true,
                        data: { transId: booktransactionId },
                        cache: false,
                        success: function (data) {

                            if (data.success) {

                                swal({
                                        title: "Success",
                                        text: "It was succesfully removed!",
                                        type: "success"
                                    },
                                    function () {

                                        $('#tbl_eventsBooking').DataTable().ajax.reload();

                                        $tablebookings.button(0).enable();
                                        $tablebookings.button(1).disable();
                                        $tablebookings.button(2).disable();
                                        $tablebookings.button(3).disable();
                                        $tablebookings.button(4).disable();

                                    });

                            }
                        }
                    });

                }
            }


            );


        }

    };



    //============= on Booking Served Schedule ==========================

    var onBookingServed = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var booktransactionId = $this.attr('data-transid');

            swal({
                    title: "Are You Sure ?",
                    text: "Confirm update booking status as served..",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, update it!',
                    closeOnConfirm: true,
                    closeOnCancel: true

                },
                function (isConfirm) {

                    if (isConfirm) {

                        $.ajax({
                            type: "post",
                            url: bookingsUrl.bookUrl_ServeBooking,
                            ajaxasync: true,
                            data: { transactionNo:booktransactionId },
                            cache: false,
                            success: function (data) {

                                if (data.success) {

                                    swal({
                                            title: "Success",
                                            text: "It was succesfully change status as served!",
                                            type: "success"
                                        },
                                        function() {

                                          

                                            //  window.location.href = bookingsUrl.bookUrl_IndexLoad;
                                        });

                                    $('#tbl_eventsBooking').DataTable().ajax.reload();

                                    $tablebookings.button(0).enable();
                                    $tablebookings.button(1).disable();
                                    $tablebookings.button(2).disable();
                                    $tablebookings.button(3).disable();
                                    $tablebookings.button(4).disable();

                                } else {

                                    swal({
                                            title: "Action Failed",
                                            text: "Unable to update status.. pls check date",
                                            type: "warning"
                                        },
                                        function () {

                                         
                                            //onrefreshBooking();

                                            //  window.location.href = bookingsUrl.bookUrl_IndexLoad;
                                        });

                                    $('#tbl_eventsBooking').DataTable().ajax.reload();

                                    $tablebookings.button(0).enable();
                                    $tablebookings.button(1).disable();
                                    $tablebookings.button(2).disable();
                                    $tablebookings.button(3).disable();
                                    $tablebookings.button(4).disable();

                                }

                            }
                            ,
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Error removing record!", "Please try again", "error");
                            }



                        });

                    }
                }
            );//end of swal

        }



    };

    //=============== end of code =========================================

  

    function loaddatatableBookings() {

        if ($.fn.DataTable.isDataTable('#tbl_eventsBooking')) {

            $('#tbl_eventsBooking').dataTable().fnDestroy();
            $('#tbl_eventsBooking').dataTable().empty();

        }

        $tablebookings = $('#tbl_eventsBooking').DataTable(
            {
                "serverSide": false,
                "processing": true,
                "language": {

                    'processing': '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only"> Loading data Pls. wait....</span>'
                },
                "paging": true,

                "order": [[3, 'asc']],

                "select": {
                    "style": 'os',
                    "selector": 'td:first-child'

                },

                "dom": "<'#bookingtop.row'<'col-sm-6'B><'col-sm-6'f>>" +
                    "<'row'<'col-sm-12'tr>>" +
                    "<'#bookingbottom.row'<'col-sm-5'l><'col-sm-7'p>>",

                "pagingType": "full_numbers",

                "ajax":
                {
                    "url": bookingsUrl.bookUrl_loadBookings,
                    "type": "Get",
                    "datatype": "json"
                },

                "columnDefs":
                [
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'width': '5%',
                        'className': 'select-checkbox',
                        'data': null,
                        'defaultContent': ''
                    },

                    {
                        'autowidth': true, 'targets': 1,
                        "data": "fullname"

                    },
                    {
                        'autowidth': true, 'targets': 2, "data": "occasion"

                    },
                    {
                        'autowidth': true, 'targets': 3,
                        "data": "startdate",
                        "type": "date",
                        "render": function (d) {
                            return moment(d).format("MMM-DD-YYYY hh:mm: A");
                        }

                    },

                    {
                        'autowidth': true, 'targets': 4,
                        "data": "packagename"

                    }
                    ,
                    {
                        'autowidth': true, 'targets': 5, "data": "trn_Id", "orderable": false, "searchable": false, "className": "text-center",
                        "mRender": function (data) {
                            return '<button class="btn btn-flat btn-sm bg-olive getpackage" id=' + data + '><i class="fa fa-cube fa-sm"></i> Menus </button>' +
                              
                                ' <button class="btn btn-flat bg-purple btn-sm getdetails" id=' + data + '><i class="fa fa-info-circle fa-sm"></i> Details </button>';
                        }
                    }

                ], 

                createdRow: function (row, data, dataIndex) {
                    $(row).attr('data-transid', data.trn_Id);
                },

                buttons:
                [
                    {
                        text: '<i class="fa fa-plus-square fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnAddBookings',
                        titleAttr: 'Create new bookings',
                        action: function () {

                            window.location.href = bookingsUrl.bookUrl_createBooking;

                        }
                    },
                    {
                        text: '<i class="fa fa-edit fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnEditBooking',
                        titleAttr: 'Modify record',
                        action: function () {

                            onEditBooking();

                        }, enabled: false
                    },
                    {
                        text: '<i class="fa fa-trash fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnRemoveBooking',
                        titleAttr: 'Remove record',
                        action: function () {

                            onremoveBooking();

                        }, enabled: false
                    },

                    {
                        text: '<i class="fa fa-archive fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnServeBooking',
                        titleAttr: 'Served Booking Status',
                        action: function () {

                            onBookingServed();

                        }, enabled: false
                    },

                    {
                        text: '<i class="fa fa-refresh fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnRefreshBooking',
                        titleAttr: 'Refresh',
                        action: function () {
                           
                            onrefreshBooking();

                        }, enabled: false
                    }

                ]

            }
        );
    }


    var onrefreshBooking = function() {
        
        $('#tbl_eventsBooking').DataTable().ajax.reload();

        $tablebookings.button(0).enable();
        $tablebookings.button(1).disable();
        $tablebookings.button(2).disable();
        $tablebookings.button(3).disable();
        $tablebookings.button(4).disable();
    };



}); //document end



var is_extendedLoc=function(data, type, full, meta) {

    var is_extended = data === true ? "checked" : "";
    return '<input type="checkbox" class="checkbox" ' + is_extended + " disabled/>";
}


//select packages location firing

$(document).on('change', '#areaSelectList', function (e) {
    e.preventDefault();

    // console.log($areasel);

    if ($.fn.dataTable.isDataTable('#tbl-packages')) {

        // console.log('DataTable');


        $('#tbl-packages').DataTable().destroy();
        $('#tbl-packages tbody').empty();

    }


    var table_SearchPackage = $('#tbl-packages').DataTable({
        destroy: true,
        responsive: true,
        bLengthChange: false,
        bFilter: false,
        ajax: {
            url:  bookingsUrl.bookUrl_areaPackages,

            data: { areaId: $(this).val() },
            type: "Get",
            datatype: "json"
        },

        "columnDefs":
            [
                {
                    'autowidth': true, 'targets': 0,
                    'data': "packagedetails"

                },
                {
                    'autowidth': true, 'targets': 1,
                    'data': "amountperPax",
                    render: $.fn.dataTable.render.number(",", ".", 2)

                },
                {
                    'width':'20%', 'targets': 2,
                    'data': "is_extended",
                    'render': is_extendedLoc,
                    'className':'dt-body-center text-center'
                        

                },
                {
                    'width':'20%', 'targets': 3,
                    'data': "packageId",
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center text-center',
                    render: function (data) {
                        //console.log(data);
                        var packageId = data;

                        return '<button class="btn bg-olive btn-flat btn-sm btn-selectPackage" type="button" id="' + packageId + '"> <i class="fa fa-check-square-o"></i> Select </button>';
                    }
                    

                }

            ]

    });

    table_SearchPackage.columns.adjust();

});

$(document).on('click', '.btn-selectPackage', function () {

   
   // console.log($('#areaSelectList option:selected').text());

    var package_Id = this.id;

    $('#hidden_packageId').val('');

    $('#packagename').val('');

    $('#packageloc_app').val('');

    $('#hidden_packageId').val(package_Id);

    $('#loc_isextended').prop('checked', false);

    $('#packageloc_app').html($('#areaSelectList option:selected').text());

    var package_Name = $(this).closest('tr').find('td:nth-child(1)').text();

    var $checkbox = $(this).closest('tr').find('input[type="checkbox"]');

     if ($checkbox.is(':checked')) {

         // console.log('checked');

         $('#loc_isextended').prop('checked', true);

     } else {
         $('#loc_isextended').prop('checked', false);
     }

    $('#packagename').val(package_Name);
    $('#modal-searchPackage').modal('hide');

    // alert(package_Name);

});


$(document).on('click', '#addDiscount', function (e) {
    e.preventDefault();
    e.stopPropagation();


    var $trId = $(this).attr('data-id');

    $.ajax({
        type: 'Get',
        url: bookingsUrl.bookUrl_AddDiscount,
        contentType: 'application/html;charset=utf8',
        data: { transactionId: $trId },
        datatype: 'html',
        cache: false,
        success: function (result) {
            var modal = $('#modalAddDiscount');
            modal.find('#modalcontent').html(result);

            modal.modal({
                backdrop: 'static',
                keyboard: false
            },
                'show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Error adding record!", "Please try again", "error");
        }


    });


});



 $(document).on('change', '#discountcode', function (e) {
        e.preventDefault();

     $.ajax({
         url: bookingsUrl.bookUrl_SelectDiscount,
         type: "Get",
         data: { discountId: $(this).val() },
         success: function (data) {
             //$('#supAlot').text(data.result);
             //console.log(data.discountdetails.discType);

             $('#discType').html(data.discountdetails.discType);
             var disc_amt;

             if (data.discountdetails.discType === 'percentage') {

                disc_amt = parseFloat(data.discountdetails.discountAmount).toFixed(2) + "%";


             } else {
                 disc_amt = currencyFormat(data.discountdetails.discountAmount);
             }

             $('#discAmt').html(disc_amt);
             $('#discdateFrom').html('');
             $('#discdateTo').html('');
             var dfrom = data.discountdetails.discStart;
             var dEnd = data.discountdetails.discEnd;
             if (dfrom != null) {
                
                 var datefrom = new Date(parseInt(dfrom.substr(6)));
               
                 $('#discdateFrom').html(datefrom.toLocaleDateString("en-US"));
                 
             }

             if (dEnd != null) {

                 var dateEnd = new Date(parseInt(dEnd.substr(6)));

           
                 $('#discdateTo').html(dateEnd.toLocaleDateString("en-US"));

             }
            
            
             //$('#discdateFrom').html();
         }
     });


 });


$(document).on('click','#btn_regDiscount',function(e) {

        e.preventDefault();

    swal({
            title: "Are You Sure ?",
            text: "Confirm Adding Discount..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Proceed!',
            closeOnConfirm: true,
            closeOnCancel: true

        },
        function (isConfirm) {

            if (isConfirm) {

                var formUrl = $('#discountform').attr('action');
                var form = $('[id*=discountform]');

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

                                //console.log(data.url);
                                swal({
                                        title: "Success",
                                        text: "It was succesfully Added!",
                                        type: "success"
                                    },
                                    function () {

                                        //$('#paymentsTable').load(data.url);

                                    

                                    });

                                setTimeout(function () {
                                    $("#modalAddDiscount").modal('hide');

                                }, 1000);

                                $('#amountDue').load(data.url);


                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            swal("Error adding record!", "Please try again", "error");

                         
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


function currencyFormat(num) {
    return num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

