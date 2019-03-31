
var $selectedObject;

$(document).ready(function () {


    if ($.fn.DataTable.isDataTable('#table_customers')) {

        $('#table_customers').dataTable().fnDestroy();
        $('#table_customers').dataTable().empty();

    }

    var customerList = $('#table_customers').DataTable(

        {


            "serverSide": false,
            "processing": true,
            "language": {

                'processing': '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only"> Loading data Pls. wait....</span>'
            },
            "paging": true,
            "dom": "<'#customertop.row'<'col-sm-4'B><'col-sm-4'l><'col-sm-4'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'#customerbottom.row'<'col-sm-5'i><'col-sm-7'p>>",
            "pagingType": "full_numbers",
            "ajax":
            {
                "url": customerUrl.url_customerloadDataTable,
                "type": "Get",
                "datatype": "json"
            },
            "columns": [
                { "data": null },
                {
                    "data": "c_Id"
                },
                {
                    "data": "lastname",
                    "className":"text-uppercase",
                    "render":function(data, type, customer) {
                        if (customer.middle === null) {
                            return customer.lastname + ' ,' + customer.firstname;
                        } else {
                            return customer.lastname + ' ,' + customer.firstname + ' ' + customer.middle;
                        }

                    }
                },
                { "data": "address"},
                { "data": "contact1"}
            ],
            "columnDefs": [
                {
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'width': '5%',
                    'className': 'select-checkbox',
                    'data': null,
                    'defaultContent': ''
                },
                { 'width': '10%', 'targets': 1 },
                { 'width': '30%', 'targets': 2 },
                { 'width': '40%', 'targets': 3 },
                { 'width': '25%', 'targets': 4 }
            ],
            createdRow: function (row, data, dataIndex) {
                $(row).attr('data-customerid', data.c_Id);
            },

            "order": [1, 'asc'],

            select: {
                style: 'os',
                selector: 'td:first-child'

            },

            buttons:
            [
                {
                    text: '<i class="fa fa-plus-square fa-fw"></i>',
                    className: 'btn btn-primary btnAddCustomer',
                    titleAttr: 'Add a new record',
                    action: function () {

                        window.location.href = customerUrl.url_customerAdd;

                    }
                },
                {
                    text: '<i class="fa fa-edit fa-fw"></i>',
                    className: 'btn btn-primary btnModifyCustomer',
                    titleAttr: 'Modify record',
                    action: function () {
                       
                        onModifyCustomer();

                    }, enabled: false
                },
                {
                    text: '<i class="fa fa-trash fa-fw"></i>',
                    className: 'btn btn-primary btnRemoveCustomer',
                    titleAttr: 'Remove record',
                    action: function () {

                        //onremoveMenu();

                    }
                    , enabled: false
                }
                ,
                {
                    text: '<i class="fa fa-print fa-fw"></i>',
                    className: 'btn btn-primary btnRemoveCustomer',
                    titleAttr: 'Print record',
                    action: function () {

                        //onremoveMenu();

                    }
                    , enabled: false
                }
            ]



        }

    );

    //============================= end of code =================================


    var onModifyCustomer = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var selectedcustomerid = $this.attr('data-customerid');

            window.location.href = customerUrl.url_modifycustomer.replace("cusId", selectedcustomerid);

        }
    };




    $('#table_customers tbody').on('click', 'tr', function () {

        $selectedObject = null;

        //alert('sadasd');

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');

            customerList.button(0).enable();
            customerList.button(1).disable();
            customerList.button(2).disable();
        }

        else {
            customerList.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

            customerList.button(0).disable();
            customerList.button(1).enable();
            customerList.button(2).enable();

          
            $selectedObject = $(this);
        }


    });


    $('#btn_savecustomer').on('click', function (e) {

        e.preventDefault();

        var form = $(this).closest('form');


        swal({
                title: "Are You Sure ?",
                text: "Confirm Saving Customer Details..",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Save it!',
                closeOnConfirm: true, closeOnCancel: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    form.submit();

                    //  showlastrecord(menulist);
                }
            });

    });


    $('#btn_modifycustomer').on('click', function (e) {

        e.preventDefault();

        var form = $(this).closest('form');

        swal({
                title: "Are You Sure ?",
                text: "Confirm Updating Customer Details..",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Update it!',
                closeOnConfirm: true, closeOnCancel: true
            },
            function (isConfirm) {
                if (isConfirm) {

                    form.submit();

                    //  showlastrecord(menulist);
                }
            });

     

    });

    $('.btnRemoveCustomer').on('click',function(e) {
        e.preventDefault();


        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var selectedcustomerid = $this.attr('data-customerid');

            console.log(selectedcustomerid);

            swal({
                    title: "Are You Sure ?",
                    text: "Confirm Removing This Customer..",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, Remove it!',
                    closeOnConfirm: true, closeOnCancel: true

                },
                function (isConfirm) {
                    if (isConfirm) {

                        $.ajax({
                            type: "post",
                            url: customerUrl.url_removecustomer,
                            ajaxasync: true,
                            data: { customerId: selectedcustomerid },
                            cache: false,
                            success: function (data) {

                                if (data.success) {

                                    swal({
                                            title: "Success",
                                            text: "It was succesfully removed!",
                                            type: "success"
                                        },
                                        function() {

                                            $('#table_customers').DataTable().ajax.reload();

                                            $('#table_customers').button(0).enable();
                                            $('#table_customers').button(1).disable();
                                            $('#table_customers').button(2).disable();

                                        });

                                }
                                else {

                                    swal("Unable to remove record!", "Please try again", "error");


                                }
                            }
                        });//-----ajax end

                    }
                }

            );

        }
       
     });


  


});//------ document end


