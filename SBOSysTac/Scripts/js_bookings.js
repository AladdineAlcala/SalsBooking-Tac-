﻿
var $selectedObject;
var $tablebookings;
var $areasel;

$(document).ready(function () {


    $('#packageselect').select2({

        dropdownParent: $('#modal-searchPackage')

    });

 

    ////Custom date validation overide for date formats
    $.validator.methods.date = function (value, element) {
        //return this.optional(element) || moment(value, "DD-MMM-YYYY HH:mm A", true).isValid();
        return this.optional(element) || moment(value, "MM/DD/YYYY HH:mm A", true).isValid();
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


    var hassuperadminrights = $('#has_superadminRight').val();

    hassuperadminrights = hassuperadminrights.toLowerCase();

   // console.log(hassuperadminrights);

    $.fn.dataTable.moment('MMM-DD-YYYY hh:mm');


    loaddatatableBookings();

  
    $tablebookings.button(4).disable();


    //=== check if user has superadmin rights

    //if (hassuperadminrights === 'true') {

    //    $tablebookings.button(4).enable();

    //}



    //  $('#tbl_eventsBooking_filter input').addClass('form-control');

    $('#btn_regbooking').on('click', function (e) {

        e.preventDefault();


        Swal.fire({
            title: "Are You Sure ?",
            text: "Confirm Saving Booking Details..",
            type: "question",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            //closeOnConfirm: true, closeOnCancel: true
        }).then((result) => {

            if (result.value) {

                var formUrl = $('#savebooking').attr('action');

                var form = $('[id*=savebooking]');

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

                                Swal.fire({
                                        title: "Success",
                                        text: "It was succesfully added!",
                                        type: "success"
                                

                                    });

                                //window.location.href = bookingsUrl.bookUrl_IndexLoad;
                                setTimeout(function () {

                                    window.location.href = bookingsUrl.bookUrl_getPackageBookingDetailsId.replace("trans_Id", data.trnsId);

                                    // $('#spinn-loader').hide();
                                }, 600);

                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            Swal.fire('Error adding record!', 'Please try again', 'error');
                        }
                    });

                } else {
                    $.each(form.validate().errorList, function (key, value) {
                        $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                        $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }


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
                document.getElementById('packageTypeSelectList').style.display = 'inline-block';
                //document.getElementById('areaSelectList').style.display = 'inline-block';
                document.getElementById('areaSelectList').style.display = 'none';
                document.getElementById('noofPaxSelectList').style.display = 'none';

                
                tableSearchPackage.columns.adjust();

                //tableSearchPackage.destroy();

                modal.modal({
                    backdrop: 'static',
                    keyboard: false
                }, 'show');

            }, error: function (xhr, ajaxOptions, thrownError) {
                Swal.fire('Error adding record!', 'Please try again', 'error');
            }

        });



    });


    //cancel booking click event

    $('#btn_cancelbooking').on('click',
        function (e) {

            e.preventDefault();

            Swal.fire({
                title: "Are You Sure ?",
                text: "You will not be able to recover this information supplied!",
                type: "question",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, I am sure!',
                cancelButtonText: "No, cancel it!",
                closeOnConfirm: false,
                closeOnCancel: false
            }).then((result) => {

                if (result.value) {

                    setTimeout(function () {

                        //window.location.href = PackageUrl.url_details.replace("pId", data.packageId);
                        window.history.back();
                        // $('#spinn-loader').hide();
                    }, 500);


                }

            });

        });


    $('#btn_cancelupdatebooking').on('click',
        function (e) {

            e.preventDefault();

            Swal.fire({
                title: "Cancel Booking Update Operation",
                text: "Yes..Cancel this operation",
                type: "info"

            });

            setTimeout(function () {

                //window.location.href = PackageUrl.url_details.replace("pId", data.packageId);
                window.history.back();
                // $('#spinn-loader').hide();
            }, 500);

         

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
         
            $tablebookings.button(5).enable();

        } else {

            $tablebookings.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

            $tablebookings.button(0).disable();
            $tablebookings.button(1).enable();
            $tablebookings.button(2).enable();
            $tablebookings.button(3).enable();

            if (hassuperadminrights === 'true') {

                $tablebookings.button(4).enable();

            }
           ;
            $tablebookings.button(5).disable();

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

                        Swal.fire({
                            title: "Operation Failed",
                            text: "No Package Available on this transaction",
                            type: "info"

                        });
                      
                    }
                }
            });


        });


    $('#tbl_eventsBooking tbody').on('click',
        'tr .get_menupackage',
        function (e) {

            e.preventDefault();

            $('#spinn-loader').show();

            var transid = ($(this).attr('id'));
            // console.log(_transid);

            $.ajax({
                type: 'GET',
                url: bookingsUrl.bookUrl_getPackageId,
                data: { transactionId: $(this).attr('id') },
                success: function (result) {
                    if (result.success) {
                        window.location.href = bookingsUrl.bookUrl_packageBooking.replace("trans_Id", transid);
                    } else {

                        Swal.fire({
                            title: "Operation Failed",
                            text: "No Package Available on this transaction",
                            type: "info"

                        });

                    }
                },
                done: function () {

                    $('#spinn-loader').hide();
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
                    Swal.fire({
                        title: "Operation Failed",
                        text: "No Package Available on this transaction",
                        type: "info"

                    });

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


        Swal.fire({
            title: "Are You Sure ?",
            text: "Confirm Update Booking Details..",
            type: "question",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            //closeOnConfirm: true, closeOnCancel: true
        }).then((result) => {

            if (result.value) {

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

                                Swal.fire({
                                    title: "Success",
                                    text: "It was succesfully updated!",
                                    type: "success"

                                    });

                        

                                setTimeout(function () {

                                    window.location.href = bookingsUrl.bookUrl_getPackageBookingDetailsId.replace("trans_Id", data.trnsId);

                                    // $('#spinn-loader').hide();
                                }, 600);

                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            Swal.fire('Error adding record!', 'Please try again', 'error');
                        }
                    });

                } else {
                    $.each(form.validate().errorList, function (key, value) {
                        $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                        $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }

          
            }
        });



    });


    //================= end of code for update booking ====================================




    // Date Created: 1-11-2019
    //================ remove booking command===============================================



    var onCancelBooking = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var booktransactionId = $this.attr('data-transid');

            Swal.fire({
                title: "You are about to cancel this booking?",
                text: "Confirm Cancellation .",
                type: "question",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Proceed Operation.!'
                //closeOnConfirm: true, closeOnCancel: true
            }).then((result) => {

                if (result.value) {

                    window.location.href = bookingsUrl.bookUrl_cancelBooking.replace("trans_Id", booktransactionId);


                }

            });//end then


            
          


        }

    };



    //============= on Booking Served Schedule ==========================

    var onBookingServed = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var booktransactionId = $this.attr('data-transid');

            Swal.fire({
                title: "Are You Sure ?",
                text: "Confirm update booking status as served..",
                type: "question",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Save it!'
                //closeOnConfirm: true, closeOnCancel: true
            }).then((result) => {

                if (result.value) {

                    $.ajax({
                        type: "post",
                        url: bookingsUrl.bookUrl_ServeBooking,
                        ajaxasync: true,
                        data: { transactionNo: booktransactionId },
                        cache: false,
                        success: function (data) {

                            if (data.success) {

                                Swal.fire({
                                        title: "Success",
                                        text: "It was succesfully change status as served!",
                                        type: "success"
                            
                                    });

                                $('#tbl_eventsBooking').DataTable().ajax.reload();

                                $tablebookings.button(0).enable();
                                $tablebookings.button(1).disable();
                                $tablebookings.button(2).disable();
                                $tablebookings.button(3).disable();
                                $tablebookings.button(4).disable();
                                //$tablebookings.button(5).disable();

                            } else {

                                Swal.fire({
                                        title: "Action Failed",
                                        text: "Unable to update status.. pls check date",
                                        type: "error"
                               
                                    });

                                $('#tbl_eventsBooking').DataTable().ajax.reload();

                                $tablebookings.button(0).enable();
                                $tablebookings.button(1).disable();
                                $tablebookings.button(2).disable();
                                $tablebookings.button(3).disable();
                                $tablebookings.button(4).disable();
                                //$tablebookings.button(5).disable();

                            }

                        }
                        ,
                        error: function (xhr, ajaxOptions, thrownError) {
                            Swal.fire('Error removing record!', 'Please try again', 'error');
                        }



                    });

                
                }
            });


        }



    };

    //=============== end of code =========================================


    // ====================== remove booking by superuser only

    var onTrashBooking = function () {

        if ($selectedObject.hasClass('selected')) {

            var $this = $selectedObject;

            var booktransactionId = $this.attr('data-transid');

            Swal.fire({
                title: "Are You Sure ?",
                text: "Confirm Remove Permanently This Booking .",
                type: "question",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Proceed for removing.!'
                //closeOnConfirm: true, closeOnCancel: true
            }).then((result) => {

                if (result.value) {

                    $.ajax({
                        type: "post",
                        url: bookingsUrl.bookUrl_removeBooking,
                        ajaxasync: true,
                        dataType: 'json',
                        data: { transId: booktransactionId },
                        cache: false,
                        success: function (data) {

                            if (data.success) {

                                Swal.fire({
                                    title: "Success",
                                    text: "It was succesfully removed!",
                                    type: "success"

                                });


                                $('#tbl_eventsBooking').DataTable().ajax.reload();

                                $tablebookings.button(0).enable();
                                $tablebookings.button(1).disable();
                                $tablebookings.button(2).disable();
                                $tablebookings.button(3).disable();
                                $tablebookings.button(4).disable();
                                //$tablebookings.button(5).disable();
                            }
                        }

                        //error 
                        ,
                        error: function (xhr, status, errorThrown) {

                            //alert(xhr.status);

                            if (xhr.status === 403) {

                                var response = $.parseJSON(xhr.responseText);

                                //  console.log(response);
                                // window.location = response.LogOnUrl;
                                Swal.fire({
                                    title: "UnAuthorized Access",
                                    text: response.Error,
                                    type: "error"

                                });

                            }
                            else
                            {
                                Swal.fire('Error removing record!', 'Please try again', 'error');
                            }

                        }

                    });


                }

            });//end then






        }


    };
    //=============== end of code =========================================


    function loaddatatableBookings() {

        if ($.fn.DataTable.isDataTable('#tbl_eventsBooking')) {

            $('#tbl_eventsBooking').dataTable().fnDestroy();
            $('#tbl_eventsBooking').dataTable().empty();

        }
        

        //var hasAdminRights = $('#has_superadminRight').val();

   

        //console.log(hasAdminRights);

        var toProperCase = function (data) {

            //debugger;
            var strdata = data;

            return strdata.replace(/\w\S*/g,
                function (txt) {
                    return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
                });
        }

       

        var trimstring=function(str) {
            return str.replace(/^\s+|\s+$/gm, '');
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
                        "data": "fullname",
                        //"render": function (data) {

                        //    return toProperCase(data);
                        //}

                        "render":function(data,type,row) {

                            if (row.no_of_lackingMenus > 0) {
                                return '<span class="name">' +
                                    toProperCase(data) +
                                    '</span> </br>' +
                                    '<label class="control-label text-danger"><i class="fa fa-bell-o"></i> ' + row.no_of_lackingMenus + ' lacking menu(s) unselected </label>';

                            } else {

                                return toProperCase(data);
                            }
                           

                        }
                      

                    },
                    {
                        'autowidth': true, 'targets': 2, "data": "occasion",
                        "render": function (data) {

                            return toProperCase(data);
                        }
                       

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
                        "data": "packagename",
                        "render": toProperCase


                    }
                    , {
                        'autowidth': true,
                        'targets': 5,
                        'data': "booktypecode",
                        'render': function (data) {

                            return (trimstring(data) === "ins" ? "Inside" : "Outside");


                        }
                    }
                    ,
                    {
                        'autowidth': true, 'targets': 6, "data": "trn_Id", "orderable": false, "searchable": false, "className": "text-center",
                        "mRender": function (data) {
                            return '<button class="btn btn-flat btn-xs bg-olive get_menupackage" id=' + data + '><i class="fa fa-cube fa-sm"></i> Menus </button>' +
                              
                                ' <button class="btn btn-flat bg-purple btn-xs getdetails" id=' + data + '><i class="fa fa-info-circle fa-sm"></i> Details </button>';
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
                    }
                    ,
                    {
                        text: '<i class="fa fa-edit fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnEditBooking',
                        titleAttr: 'Modify Booking',
                        action: function () {

                            onEditBooking();

                        }, enabled: false
                    },
                    {
                        text: '<i class="fa fa-times-circle-o fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnRemoveBooking',
                        titleAttr: 'Cancel Booking',
                        action: function () {

                            onCancelBooking();

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
                        text: '<i class="fa fa-trash fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnTrashBooking',
                        titleAttr: 'Remove Booking Permanently',
                        action: function () {
                          
                            onTrashBooking();

                        }, enabled: false
                    }
                    ,

                    {
                        text: '<i class="fa fa-refresh fa-sm fa-fw"></i>',
                        className: 'btn btn-primary btn-sm btnRefreshBooking',
                        titleAttr: 'Refresh',
                        action: function () {
                           
                            onrefreshBooking();

                        }, enabled: true
                    }

                ]

            }
        );
    }





    onrefreshBooking = function() {
        
        $('#tbl_eventsBooking').DataTable().ajax.reload();

        $tablebookings.button(0).enable();
        $tablebookings.button(1).disable();
        $tablebookings.button(2).disable();
        $tablebookings.button(3).disable();
        $tablebookings.button(4).disable();
        //$tablebookings.button(5).disable();
    };



  



});//======== document end =============================



var is_extendedLoc=function(data, type, full, meta) {

    var is_extended = data === true ? "checked" : "";
    return '<input type="checkbox" class="chkbox_isExtended" ' + is_extended + " disabled/>";
}

var is_ActivePackage = function (data, type, full, meta) {

    var isActive = data === true ? "checked" : "";
    return '<input type="checkbox" class="chkbox_isActive" ' + isActive + " disabled/>";
}

//select packages location firing

$(document).on('change', '#' +
  'areaSelectList' +
    '', function (e) {

        e.preventDefault();
        e.stopPropagation();

        var selId = $(this).attr('id');

        $areasel = $(this).val();

        LoadDataTableSearch($areasel, selId);

    });

$(document).on('change', '#' +
    'packageTypeSelectList' +
    '', function (e) {

        e.preventDefault();
        e.stopPropagation();

        debugger;

        var selId = $(this).attr('id');

        if ($(this).val() === "regular") {

            document.getElementById('areaSelectList').selectedIndex = 0;

            if ($.fn.dataTable.isDataTable('#tbl-packages')) {

                // console.log('DataTable')

                $('#tbl-packages').DataTable().destroy();
                $('#tbl-packages tbody').empty();

            }

            $('#tbl-packages').DataTable({
                 bLengthChange: false,
                 bFilter: false
             });

            document.getElementById('areaSelectList').style.display = 'inline-block';

        } else {

            document.getElementById('areaSelectList').style.display = 'none';

            LoadDataTableSearch($(this).val(), selId);
        }
    

    });

$(document).on('change', '#' +
    'noofPaxSelectList' +
    '', function (e) {

        e.preventDefault();
        e.stopPropagation();

        var selId = $(this).attr('id');
        //alert(selId);
        LoadDataTableSearch($(this).val(), selId);

});



var LoadDataTableSearch = function(data,selectedId) {

    var searchstring = data;

    var seleId = selectedId;

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
        order: [],

        ajax: {
            url: bookingsUrl.bookUrl_getResultSearchPackages,
            data: { searchstr: searchstring, selectedId: seleId},
            type: "Get",
            datatype: "json"
        },

        "columnDefs":
        [
            {
                'width': '30%', 'targets': 0,
                'data': "packagedetails"

            },
            {
                'autowidth': true, 'targets': 1,
                'data': "amountperPax",
                'className': 'dt-body-center text-right',
                render: $.fn.dataTable.render.number(",", ".", 2)

            },

            {
                'width': '15%', 'targets': 2,
                'data': "isActive",
                'render': is_ActivePackage,
                'className': 'dt-body-center text-center'


            },
            {
                'width': '20%', 'targets': 3,
                'data': "is_extended",
                'render': is_extendedLoc,
                'className': 'dt-body-center text-center'


            },

            {
                'width': '10%', 'targets': 4,
                'data': "packageId",
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center text-center',
                render: function (data, type, row) {
                    //console.log(data);
                    var packageId = data;

                    //console.log(row.isActive);

                    if (row.isActive === true) {

                        return '<button class="btn bg-olive btn-flat btn-sm btn-selectPackage" type="button" id="' + packageId + '"> <i class="fa fa-check-square-o"></i> Select </button>';

                    } else {

                        return '<button class="btn bg-olive btn-flat btn-sm btn-selectPackage " type="button" disabled="disabled"> <i class="fa fa-check-square-o"></i> Select </button>';

                    }


                    // alert(row.isActive);

                }


            }

        ]

    });

    table_SearchPackage.columns.adjust();

}



$(document).on('click', '.btn-selectPackage', function () {

    debugger;

   // console.log($('#areaSelectList option:selected').text());

    var package_Id = this.id;

    $('#hidden_packageId').val('');

    $('#packagename').val('');

    $('#packageloc_app').val('');

    $('#hidden_packageId').val(package_Id);
    
    if ($areasel == null) {
        $('#hidden_areaid').val(0);
    } else {
        $('#hidden_areaid').val($areasel);
    }
   

    $('#loc_isextended').prop('checked', false);


    if ($('#packageTypeSelectList option:selected').text() !== "vip") {
        $('#packageloc_app').html("Vip");
    } else {
        $('#packageloc_app').html($('#areaSelectList option:selected').text());
    }
    var package_Name = $(this).closest('tr').find('td:nth-child(1)').text();

    var $checkbox = $(this).closest('tr').find('td:nth-child(4)').find('input[type="checkbox"]');


    //var $checkbox = $(this).closest('tr').find('input[type="checkbox"]');

     if ($checkbox.is(':checked')) {

          //console.log('checked');

         $('#loc_isextended').prop('checked', true);

     } else {
         $('#loc_isextended').prop('checked', false);

         //console.log('unchecked');
     }

   
     if ($areasel === null) {
         $('#loc_isextended').removeClass('disabled');
       
     } else {
         $('#loc_isextended').addClass('disabled');
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
        }

            ,
            error: function (xhr, status, errorThrown) {

                // alert(xhr.status);

                if (xhr.status === 403) {


                    var response = $.parseJSON(xhr.responseText);

                  //  console.log(response);
                    //   window.location = response.LogOnUrl;
                    Swal.fire('Unable to redirect', response.Error, 'error');

                } else {
                    Swal.fire('Error adding record!', 'Please try again', 'error');
                }
            
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


 $(document).on('click', '#btn_regDiscount', function (e) {

     e.preventDefault();

     Swal.fire({
         title: "Are You Sure ?",
         text: "Confirm Adding Discount..",
         type: "question",
         showCancelButton: true,
         confirmButtonColor: '#3085d6',
         cancelButtonColor: '#d33',
         confirmButtonText: 'Yes, Save it!'
         //closeOnConfirm: true, closeOnCancel: true
     }).then((result) => {

         if (result.value) {

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
                             Swal.fire({
                                 title: "Success",
                                 text: "It was succesfully Added!",
                                 type: "success"


                             });

                             setTimeout(function () {
                                 $("#modalAddDiscount").modal('hide');

                             }, 600);

                             $('#amountDue').load(data.url);


                         }
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         Swal.fire('Error adding record!', 'Please try again', 'error');


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
     });

});



 $(document).on('click', '#remove_discount', function (e) {
    e.preventDefault();
    e.stopPropagation();


    var selectedTransId = $(this).attr('data-id');

    Swal.fire({
         title: "Are You Sure ?",
         text: "Confirm Removing Discount..",
         type: "question",
         showCancelButton: true,
         confirmButtonColor: '#3085d6',
         cancelButtonColor: '#d33',
         confirmButtonText: 'Yes,Proceed Operation!'
         //closeOnConfirm: true, closeOnCancel: true
     }).then((result) => {

         if (result.value) {

             $.ajax({
                 type: "post",
                 url: bookingsUrl.bookUrl_RemoveDiscount,
                 ajaxasync: true,
                 dataType: 'json',
                 data: {transId: selectedTransId},
                 cache: false,
                 success: function (data) {

                     if (data.success) {

                         Swal.fire({
                             title: "Success",
                             text: "It was succesfully removed!",
                             type: "success"


                         });

                         $('#amountDue').load(data.url);

                       //  window.page.reload();
                     }

                 }
                 ,
                 error: function (xhr, status, errorThrown) {

                     // alert(xhr.status);

                     if (xhr.status === 403) {


                         var response = $.parseJSON(xhr.responseText);

                         //  console.log(response);
                        // window.location = response.LogOnUrl;

                         Swal.fire({
                             title: "Unable to proccess request",
                             text: response.Error,
                             type: "error"


                         });

                     }
                 }


             });//-----ajax end


         }

     });//end then


 });

 $(document).on('change', 'input[name="radiosearchpackageoption"]', function (e) {
     e.preventDefault();
     e.stopPropagation();

     var selected_id = $(this).attr('id');
   
     if (selected_id === 'radType') {
        
         document.getElementById('areaSelectList').style.display = 'none';
         document.getElementById('packageTypeSelectList').style.display = 'inline-block';
         document.getElementById('noofPaxSelectList').style.display = 'none';

         document.getElementById('packageTypeSelectList').selectedIndex = 0;

         //} else if (selected_id === 'radlocation') {

         //    document.getElementById('areaSelectList').style.display = 'inline-block';
         //    document.getElementById('packageTypeSelectList').style.display = 'none';
         //    document.getElementById('noofPaxSelectList').style.display = 'none';


     } else if (selected_id === 'radpax') {
         
         document.getElementById('noofPaxSelectList').style.display = 'inline-block';
         document.getElementById('packageTypeSelectList').style.display = 'none';
         document.getElementById('areaSelectList').style.display = 'none';

         document.getElementById('noofPaxSelectList').selectedIndex = 0;
     }

     if ($.fn.dataTable.isDataTable('#tbl-packages')) {

         // console.log('DataTable');


         $('#tbl-packages').DataTable().destroy();
         $('#tbl-packages tbody').empty();

     }
     var tableSearchPackage = $('#tbl-packages').DataTable({ bLengthChange: false, bFilter: false });

 });




function currencyFormat(num) {
    return num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

