var $selectedObject;

$(document).ready(function () {

    if ($.fn.DataTable.isDataTable('#table_course')) {

        $('#table_course').dataTable().fnDestroy();
        $('#table_course').dataTable().empty();

    }


    //----------- datatable load data -------------------

    $('#table_course').DataTable({
        paging: true,
        "lengthChange": false,
         //"dom": '<"top"Bf>rt<"bottom"lp><"clear">',

        "dom": "<'#coursetop.row'<'col-sm-6'B><'col-sm-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'#coursebottom.row'<'col-sm-5'i><'col-sm-7'p>>",


        "pagingType": "simple_numbers",
        "columnDefs": [
            { "orderable": false, "targets": 0, "className": 'select-checkbox', "data": null, "defaultContent": '' },
            { "orderable": true, "targets": 1 },
            { "orderable": true, "targets": 2 },
            { "width": "5%", "targets": 0 },
            { "width": "9%", "targets": 1 },
            { "width": "18%", "targets": 4 }
        ],
        select: {
            style: 'os',
            selector: 'td:first-child'
        },
        //dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus-square fa-fw"></i>',
                className: 'btn btn-primary btnAddJob',
                titleAttr: 'Add a new record',
                action: function (dt, node, config) {

                    window.location.href = courseUrl.urlAdd;
                }
            },
            {
                text: '<i class="fa fa-edit fa-fw"></i>',
                className: 'btn btn-primary EditJob',
                titleAttr: 'Modify record',
                action: function (e, dt, node, config) {
                  
                    onmodifyCourse();
                }, enabled: false
            },
            {
                text: '<i class="fa fa-trash fa-fw"></i>',
                className: 'btn btn-primary btnRemoveJob',
                titleAttr: 'Remove record',
                action: function (e, dt, node, config) {

                    onremoveCourse();

                }
                , enabled: false
            }
        ]


    });

    //var is_maincourse = function (data, type, full, meta) {

    //    var is_extended = data === true ? "checked" : "";
    //    return '<input type="checkbox" class="checkbox" ' + is_extended + " disabled/>";
    //}


   



    var tablecourse = $('#table_course').DataTable();

    $('#table_course tbody').on('click', 'tr', function () {

        $deleteObj = null;

        //alert('sadasd');

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');

            tablecourse.button(0).enable();
            tablecourse.button(1).disable();
            tablecourse.button(2).disable();
        }

        else {
            tablecourse.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

            tablecourse.button(0).disable();
            tablecourse.button(1).enable();
            tablecourse.button(2).enable();

            $deleteObj = $(this);
        }


    });


    function onremoveCourse() {

        if ($deleteObj.hasClass('selected')) {

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

                        var cId = $deleteObj.closest("tr").find('td:eq(1)').text();
                        var row = $deleteObj.closest('tr');
                        var api = $('#table_course').DataTable();

                        $.ajax({

                            type: "post",
                            url: courseUrl.urlRemove,
                            ajaxasync: true,
                            data: { courseId: cId },
                            success: function (data) {

                                if (data.success) {

                                    swal({
                                        title: 'Deleted!',
                                        text: data.deletedcourse + ' was deleted successfully.',
                                        type: 'warning'
                                    });
                                    api.row(row).remove().draw();

                                    //===== refresh button delete and edit ======================

                               
                                } else {

                                    swal("Unable to remove this record! course is in used", "Please try again or contact sys admin", "error");
                                  
                                }

                                tablecourse.button(0).enable();
                                tablecourse.button(1).disable();
                                tablecourse.button(2).disable();
                            },
                            error: function (data) {
                                alert(data);
                            }

                        });
                    }
                });
        }
    }


    var onmodifyCourse = function() {
        
        if ($deleteObj.hasClass('selected')) {

            swal({
                    title: "Update this record?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Ok",
                    closeOnConfirm: true,
                    closeOnCancel: true

                },
                function (isConfirm) {
                    if (isConfirm) {

                        var cId = $deleteObj.closest("tr").find('td:eq(1)').text();
                        //var row = $deleteObj.closest('tr');
                        //var api = $('#table_course').DataTable();

                        window.location.href = courseUrl.urlUpdateCourse.replace("cId", cId);
                    }
                });
        }

    };


});//===document end

$(document).on('click','#btn_regCourse', function (e) {

    e.preventDefault();

    var form = $(this).closest('form');


    swal({
            title: "Are You Sure ?",
            text: "Confirm Saving Course..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            closeOnConfirm: true, closeOnCancel: true
        },
        function (isConfirm) {
            if (isConfirm) {
                //form.submit();

                var formUrl = $('#create_course').attr('action');

                //var form = $('[id*=create_course]');

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
                        //contentType:'application/json;charset=utf-8',
                        success: function (data) {
                            if (data.success) {

                                //alert(data.success);

                                swal({
                                        title: "Success",
                                        text: "It was succesfully added!",
                                        type: "success"
                                    },
                                    function () {

                                    });


                                setTimeout(function () {

                                    window.location.href = courseUrl.urlcourseIndex;

                                    // $('#spinn-loader').hide();
                                }, 500);


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


            }
        });

});