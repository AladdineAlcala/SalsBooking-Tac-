$(document).ready(function () {

    var $selectedObj = "";

    if ($.fn.DataTable.isDataTable('#table_menu')) {

        $('#table_menu').dataTable().fnDestroy();
        $('#table_menu').dataTable().empty();

    }

    var menulist = $('#table_menu').DataTable(

        {

            "serverSide": true,
            "processing": true,
            "paging": true,
            "bInfo": false,
            //"dom": '<"top"B>rt<"bottom"lp><"clear">',

            "dom": "<'#menutop.row'<'col-sm-6'B>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'#menubottom.row'<'col-sm-5'l><'col-sm-7'p>>",

            "pagingType": "full_numbers",
            "ajax":
            {
                "url": menuUrl.menuurl_loadMenu,
                "type": "POST",
                "datatype": "json"
            },
            "columns": [
                { "data": null },
                { "data": "menu_Id", "name": "menu_Id" },
                { "data": "menudesc", "name": "menudesc" },
                { "data": "coursecategory", "name": "coursecategory" },
                { "data": "departmentincharge", "name": "departmentincharge" }
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
                { 'width': '35%', 'targets': 2 },
                { 'width': '20%', 'targets': 3 },
                { 'width': '15%', 'targets': 4 }
            ],
            "order": [1, 'asc'],

            select: {
                style: 'os',
                selector: 'td:first-child'

            },

            buttons:
            [
                {
                    text: '<i class="fa fa-plus-square fa-fw"></i>',
                    className: 'btn btn-primary btnAddMenu',
                    titleAttr: 'Add a new record',
                    action: function (dt, node, config) {

                        window.location.href = menuUrl.menuurl_add;

                    }
                },
                {
                    text: '<i class="fa fa-edit fa-fw"></i>',
                    className: 'btn btn-primary btnEditMenu',
                    titleAttr: 'Modify record',
                    action: function (e, dt, node, config) {
                        onEditMenu();
                    }
                    , enabled: false
                },
                {
                    text: '<i class="fa fa-trash fa-fw"></i>',
                    className: 'btn btn-primary btnRemoveMenu',
                    titleAttr: 'Remove record',
                    action: function (e, dt, node, config) {

                        onremoveMenu();

                    }
                    , enabled: false
                }
            ]



        }

    );

    //============================= end of code =================================

    $('#table_menu tbody').on('click', 'tr', function (e) {

        e.stopPropagation();



        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');

            menulist.button(0).enable();
            menulist.button(1).disable();
            menulist.button(2).disable();

        }

        else {
            menulist.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

            menulist.button(0).disable();
            menulist.button(1).enable();
            menulist.button(2).enable();

            $selectedObj = $(this);
        }

        console.log($selectedObj);

    });


    $('#btnSearch').click(function () {


        menulist.columns(3).search($('#courseList').val().trim());

        menulist.columns(2).search($('#searchmenu').val().trim());


        menulist.draw();
    });


    $('#btn_savemenu').on('click', function (e) {

        e.preventDefault();

        swal({
                title: "Are You Sure ?",
                text: "Confirm Adding Menu..",
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

                    var formUrl = $('#createMenu').attr('action');
                    var form = $('[id*=createMenu]');

                    $.validator.unobtrusive.parse(form);
                    form.validate();


                    if (form.valid()) {


                      //  $('#spinn-loader').show();

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
                                           
                                          //  console.log(menuUrl.menuurl_Index);
                                            
                                            setTimeout(function () {
                                                window.location.href = menuUrl.menuurl_Index;
                                               // $('#spinn-loader').hide();
                                            }, 500);

                                        });

                                   
                                 

                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Error adding record!", "Please try again", "error");

                                //$('#spinn-loader').hide();
                            }
                        });

                    }
                    else {

                        //$('#spinn-loader').hide();

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


    $('select[name=CourserId]').on('change', function () {


        if ($(this).val() != '') {

            //alert('asdas');
            $('#validationcourse').hide();

        }

    });

    $('select[name=deptId]').on('change', function () {


        if ($(this).val() != '') {
            $('#validationdept').hide();

        }

    });

    function onremoveMenu() {

        if ($selectedObj.hasClass('selected')) {


            swal({
                    title: "Remove this record?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Ok",
                    closeOnConfirm: true,
                    closeOnCancel: true
                },
                function (isConfirm) {

                    if (isConfirm) {

                        var _menuId = $selectedObj.closest("tr").find('td:eq(1)').text();

                        var row = $selectedObj.closest('tr');
                        var api = $('#table_menu').DataTable();


                        $.ajax({

                            type: 'post',
                            url: menuUrl.menuurl_remove,
                            ajaxasync: true,
                            data: { menuId: _menuId },
                            success: function (data) {
                                if (data.success) {

                                    swal({
                                        title: 'Deleted!',
                                        text: data.deletedMenu + ' was deleted successfully.',
                                        type: 'warning'
                                    });

                                    api.row(row).remove().draw();

                              

                                } else {
                                    
                                    swal("Unable to remove this record! course is in used", "Please try again or contact sys admin", "error");

                    
                                }

                                menulist.button(0).enable();
                                menulist.button(1).disable();
                                menulist.button(2).disable();
                            }
                        });
                    }


                });
        }
    }

    var onEditMenu = function () {

        if ($selectedObj.hasClass('selected')) {

            var _menuId = $selectedObj.closest("tr").find('td:eq(1)').text();

            window.location.href = menuUrl.menuurl_modify.replace("menucode", _menuId);

        }

    };


    $('#btn_modifymenu').on('click', function (e) {

        e.preventDefault();

        swal({
                title: "Are You Sure ?",
                text: "Confirm Updating Menu..",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Update it!',
                closeOnConfirm: true,
                closeOnCancel: true

            },
            function (isConfirm) {

                if (isConfirm) {

                    var formUrl = $('#menumodifyform').attr('action');
                    var form = $('[id*=menumodifyform]');

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
                                            text: "It was succesfully updated!",
                                            type: "success"
                                        },
                                        function () {

                                    

                                        });


                                    setTimeout(function () {
                                        window.location.href = menuUrl.menuurl_Index;
                                      
                                    }, 300);

                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Error adding record!", "Please try again", "error");

                               
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
   
});

//$(document).on('click',
//    'button.btnAddMenu',
//    function () {
       

//        $('.sidebar-menu ul li').find('a').each(function () {

//            var link = new RegExp($(this).attr('href')); //Check if some menu compares inside your the browsers link
           
//            if (link.test(document.location.href)) {
                
//                if ($(this).parents().hasClass('active')) {

//                    console.log($(this).parents('li'));

//                    $(this).parents('li').addClass('menu-open');
//                    $(this).parents().addClass("active");
//                    $(this).addClass("active"); //Add this too
//                }
//            }
//        });

       
//    });
