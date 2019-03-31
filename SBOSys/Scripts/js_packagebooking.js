
$tblMainCourse = null;
$selectedObj = null;
$selectedId = null;

$(document).on('click', '#tbl-maincourse tbody tr', function () {

    $selectedObj = $(this);

    $selectedId = "";

    if ($selectedObj.hasClass('selected')) {
        var tr = $(this).closest('tr');
        var id = tr.children('td:eq(0)').attr('data-id');

        $selectedId = id;
    }

    //  console.log($selectedId);

});

function LoadDataTabletoModal() {


    if ($.fn.dataTable.isDataTable('#tbl-maincourse')) {

        // console.log('DataTable');


        $('#tbl-maincourse').DataTable().destroy();
        $('#tbl-maincourse tbody').empty();

    }

    $tblMainCourse = $('#tbl-maincourse').DataTable(
        {
            bLengthChange: false,

            select: {
                style: 'os',
                //style: 'multi',
                selector: 'td:first-child'
            }
            ,

            "dom": "<'row'<'col-sm-6'B><'col-sm-6'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-6'i><'col-sm-6'p>>",


            "ajax":
            {
                "url": PackageBookingUrl.urlMenuList,
                "type": "Get",
                "datatype": "json"
            },

            //  "aoColumns": [{   "sTitle": "<input type='checkbox' id='selectAll'></input>"}],

            "columnDefs":
            [
                {
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'width': '2%',
                    'className': 'select-checkbox',
                    'data': null,
                    'defaultContent': ''


                },
                {
                    'autowidth': true, 'targets': 1,
                    "data": null,
                    "render": function (data, type, full) {

                        if (full.isMainMenu === true) {

                            return full.menu_name + " ( Main Menu )";
                        } else {

                            return full.menu_name;
                        }
                    }

                },
                {
                    'autowidth': true, 'targets': 2,
                    "data": "course"
                }
                //,
                //{

                //    "data": "menuId",'visisble':false,'targert':3
                //}


            ],
            createdRow: function (row, data, indice) {
                $(row).find("td:eq(0)").attr('data-id', data.menuId);
            },
            buttons:
            [
                {
                    text: '<i class="fa fa-plus-square fa-lg fa-fw"></i>',
                    className: 'btn btn-primary btnAddBookings',
                    titleAttr: 'Add Menu to Package',
                    action: function (e, dt, node, config) {



                    }
                }
            ]

        });
}





$(document).on('click', '#add_maincourse', function (e) {
    e.preventDefault();

    $.ajax({
        type: 'Get',
        url: PackageBookingUrl.urlSearchpackagebooking,
        contentType: 'application/html;charset=utf8',
        data: { transactionId: $(this).data('id') },
        datatype: 'html',
        cache: false,
        success: function (result) {

            var modal = $('#modal-searchPackageBooking');

            modal.find('#modalcontent').html(result);

            LoadDataTabletoModal();


            modal.modal({
                backdrop: 'static',
                keyboard: false
            }, 'show');

        }, error: function (xhr, ajaxOptions, thrownError) {
            swal("Error adding record!", "Please try again", "error");
        }



    });



});

//============ add selected menus for package booking ======================

$(document).on('click', '.btnAddBookings', function (e) {
    e.preventDefault();

    if ($selectedObj == null) {

        alert('No Menus Selected');
    }

    if ($selectedObj.hasClass('selected')) {

        var trId = $("#hdntransId").val();

        //  console.log(trId);

        $.ajax({
            type: "post",
            url: PackageBookingUrl.urlAddMenustoBooking,
            ajaxasync: true,
            data: { transacId: trId, menuId: $selectedId },
            cache: false,
            success: function (data) {

                if (data.isRecordExist) {

                    swal({
                        title: 'Unable to process!',
                        text: data.message,
                        type: 'warning'
                    },function() {
                        
                    });


                } else {

                    swal({
                        title: "Success",
                        text: "It was succesfully added!",
                        type: "success"
                    },
                        function () {

                          

                        });

                    setTimeout(function () {

                        LoadDataTabletoModal();
                        $('#bookmenus').load(data.url);

                        // $('#spinn-loader').hide();
                    }, 1000);

                    $selectedObj = null;
                    $selectedId = null;

                  

         
                }
            }

        });


    }


});

$(document).on('click', '#addon_Information', function (e) {

    e.preventDefault();

    $.ajax({
        type: 'Get',
        url: PackageBookingUrl.urlAddOnsInformation,
        contentType: 'application/html;charset=utf8',
        data: { transactionId: $(this).data('id') },
        datatype: 'html',
        cache: false,
        success: function (result) {
            var modal = $('#modal-Addons');

            modal.find('#modal_addoncontents').html(result);

            modal.modal({
                backdrop: 'static',
                keyboard: false
            }, 'show');
        }

    });
});

$(document).on('click',
    '#btn_saveaddOns',
    function (e) {
        e.preventDefault();

        swal({
            title: "Are You Sure ?",
            text: "Confirm Saving Addons..",
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


                    var formUrl = $('#save_addons').attr('action');

                    var form = $('[id*=save_addons]');

                    console.log(form);

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
                                        function () {

                                            //load partialview


                                        });

                                    $('#addons').load(data.url);
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Error adding record!", "Please try again", "error");
                            }
                        });
                    } else {
                        $.each(form.validate().errorList,
                            function (key, value) {
                                $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                                $errorSpan.html("<span style='color:#a94442'>" + value.message + "</span>");
                                $errorSpan.show();
                            });
                    }

                }
            }
        );

    });


// remove booking menus
$(document).on('click', '#menu_remove', function (e) {
    e.preventDefault();

    var menuNo = $(this).attr("data-menuid");

    swal({
        title: "Are You Sure ?",
        text: "Confirm Removing This Menu..",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Remove it!',
        closeOnConfirm: true,
        closeOnCancel: true

    }, function (isConfirm) {

        if (isConfirm) {

            console.log(menuNo);

            $.ajax({

                type: "post",
                url: PackageBookingUrl.urlRemoveBookingMenus,
                ajaxasync: true,
                data: { bookmenuNo: menuNo },
                cache: false,
                success: function (data) {

                    if (data.success) {

                        swal({
                                title: "Success",
                                text: "It was succesfully removed!",
                                type: "success"
                            },
                            function() {

                                //  window.location.href = bookingsUrl.bookUrl_IndexLoad;
                            });


                        $('#bookmenus').load(data.url);

                    } else {
                        swal({
                            title: 'ERROR!',
                            text: 'Unable to remove record..',
                            type: 'warning'
                        });
                    }
                }
                ,
                error: function (xhr, ajaxOptions, thrownError) {
                    swal("Error removing record!", "Please try again", "error");
                }

            });
        }
    }


    );


});


$(document).on('click', '#addons_remove', function (e) {
    e.preventDefault();

    var addonId = $(this).attr("data-id");

    swal({
        title: "Are You Sure ?",
        text: "Confirm Removing This Addon..",
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

                $.ajax({

                    type: "post",
                    url: PackageBookingUrl.urlRemoveBookingAddOns,
                    ajaxasync: true,
                    data: { addonNo: addonId },
                    cache: false,
                    success: function (data) {

                        if (data.success) {

                            swal({
                                title: "Success",
                                text: "It was succesfully removed!",
                                type: "success"
                            });


                            $('#addons').load(data.url);

                            setTimeout(function () { $("#modal-Addons").modal('hide') }, 300);
                        }
                        else {
                            swal({
                                title: 'ERROR!',
                                text: 'Unable to remove record..',
                                type: 'warning'
                            });
                        }

                    }
                });
            }

        }
    );
});

$(document).on('click', '#btn_canceladdon', function (e) {
    e.preventDefault();

   // alert('asds');

    setTimeout(function () { $("#modal-Addons").modal('hide') }, 300);
});


