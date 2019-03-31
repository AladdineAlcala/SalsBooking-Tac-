var $tblCusBookings = null;

$(document).ready(function () {

    $('#customerbookinginquiry').on('click', function (e) {

        e.preventDefault();

        var _cusId = $('#hiddencusId').val();

   


        $.ajax({
            type: 'Get',
            url: inquiryUrl.inquiryUrl_customerbookings,
            contentType: 'application/html;charset=utf8',
            beforeSend:function() {
                $('#spinn-loader').show();
            },
            datatype: 'html',
            cache: false,
            success: function (result) {

                $('#filteredRecurd').html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal("Error on retrieving record!", "Please try again", "error");
            }


        }).done(function() {

            loadDataTableBookings(_cusId);

            $('#spinn-loader').hide();
        });

    });


    function loadDataTableBookings(id) {

        if ($.fn.dataTable.isDataTable('#tbl-cusBookings')) {

            $('#tbl-cusBookings').DataTable().destroy();
            $('#tbl-cusBookings tbody').empty();

        }

        console.log('DataTable');

        $tblCusBookings = $('#tbl-cusBookings').DataTable({

            "serverSide": false,
            "processing": true,
            "language": {

                'processing': '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only"> Loading data Pls. wait....</span>'
            },
            "ajax":
            {
                "url": inquiryUrl.inquiryUrl_loadbookingsbycustomer,
                "type": "Get",
                "data": {cusId:id},
                "datatype": "json"
            },
            "columnDefs":
                [
                    {
                        'autowidth': true, 'targets': 0,
                        "data": "transId"
                    },
                    {
                        'autowidth': true, 'targets': 1,
                        "data": "bookdatetime",
                        "type": "date",
                        "render": function (d) {
                            return moment(d).format("MMM-DD-YYYY hh:mm: A");
                        }

                    },
                    {
                        'autowidth': true, 'targets': 2,
                        "data": "occasion"
                    },
                    {
                        'autowidth': true, 'targets': 3,
                        "data": "venue"
                    },
                    {
                        'autowidth': true, 'targets': 4,
                        "data": "package"
                    },
                    {
                        'autowidth': true,
                        'targets': 5,
                        "orderable":false,
                        "className": "text-right",
                        "data": "packageDue",
                        render: $.fn.dataTable.render.number(",", ".", 2)

                    }

                ]
            

        });


    }//end function
});

//$(document).ajaxStart(function() {
//    $('#spinn-loader').show();
//}).ajaxStop(function() {
//    $('#spinn-loader').show();
//});