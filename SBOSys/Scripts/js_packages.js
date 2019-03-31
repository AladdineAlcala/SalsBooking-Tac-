$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $('#btnAddPackage').on('click', function (e) {

      //  e.preventDefault();
        e.stopPropagation();

       // alert('asdasd');

        $('#newpackageform').load(this.href,
            function () {
                
                reparseform();
            });

    });


    function reparseform() {
        $("#new_packageForm").removeData("validator");
        $("#new_packageForm").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse('#new_packageForm');
        //console.log('sadasd');

    }


});

function formatRepo(item) {
    var markup = "<table class='item-result'><tr>";
    if (item.text !== undefined) {
        markup += "<div class='item-name' data-jtext=" + item.text + " data-jid=" + item.id + ">" + item.text + "</div>";
    }
    markup += "</td></tr></table>";
    return markup;
}

function formatRepoSelection(item) {
    return item.text;
}


$(document).on('click','#btn_savePackages', function (e) {

    e.preventDefault();

   var form = $(this).closest('form');

  
    swal({
            title: "Are You Sure ?",
            text: "Confirm Saving Packages..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            closeOnConfirm: true, closeOnCancel: true

        },
        function (isConfirm) {
            if (isConfirm) {
               
                var formUrl = $('#new_packageForm').attr('action');

                //var form = $('[id*=new_packageForm]');

                //console.log(form);

                //form.removeData("validator");
                //form.removeData("unobtrusiveValidation");

                $.validator.unobtrusive.parse(form);
                form.validate();

                if (form.valid()) {

                    //console.log('valid form');

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
                                         text: "It was succesfully Added!",
                                         type: "success"
                                     },
                                     function () {

                                         //('#packageform').load(data.url);

                                       


                                     });

                                setTimeout(function () {

                                    $('#newpackageform').load(data.url);

                                    // $('#spinn-loader').hide();
                                }, 1000);

                               // ('#newpackageform').load(data.url);

                             }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            swal("Error adding record!", "Please try again", "error");
                        }
                    });

                } else {
                    $.each(form.validate().errorList, function (key, value) {
                        $errorSpan = $("span[data-valmsg-for='" + value.element.id + "']");
                        $errorSpan.html("<span style='color:red'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }

                //  showlastrecord(menulist);
            }
        });

});

//form PackageBody Save Method
$(document).on('click', '#btn_savePackagedetails', function(e) {
    e.preventDefault();
    e.stopPropagation();

    var form = $(this).closest('form');


    swal({
            title: "Are You Sure ?",
            text: "Confirm Saving Package Details..",
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
                var formUrl = $('#form_packagebody').attr('action');
                //console.log(formUrl);

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
                               

                              // window.location.href = PackageUrl.url_details.replace("pId", data.packageId);

                               swal({
                                       title: "Success",
                                       text: "It was succesfully Added!",
                                       type: "success"
                                   },
                                   function () {

                             
                                    
                                     

                                   });


                               setTimeout(function () {

                                   window.location.href = PackageUrl.url_details.replace("pId", data.packageId);

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
                        $errorSpan.html("<span style='color:red'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }

                //  showlastrecord(menulist);
            }
        });
});



function PrivateCheck() {
    $(document).on('click','#chkExtended',function(e) {

        e.stopPropagation();

        if ($('[id*=chkExtended]').is(':checked')) {

            $('[id*=extAmount]').removeAttr('disabled');
            $('[id*=extAmount]').focus();

        } else {
            
            $('[id*=extAmount]').attr('disabled','disabled');
        }


      
    });
}

$(document).on('click', '#btnSavelocation', function (e)
{
    e.preventDefault();

   
    swal({
            title: "Are You Sure ?",
            text: "Confirm Saving Location Details..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Save it!',
            closeOnConfirm: true, closeOnCancel: true

        },
        function (isConfirm) {
            if (isConfirm) {

                var formUrl = $('#packagelocationform').attr('action');

                var form = $('[id*=packagelocationform]');

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
                                        text: "It was succesfully Added!",
                                        type: "success"
                                    },
                                    function () {


                                    });

                                setTimeout(function () {

                                    location.href = PackageUrl.url_details.replace("pId", data.packageId);
                                    $('[id*=modal-addpackagecoverage]').modal('hide');

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
                        $errorSpan.html("<span style='color:yellow'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }

                //  showlastrecord(menulist);
            }
        });

});

$(document).on('click','#package_remove',function(e) {

        e.preventDefault();

        var _packageId = $(this).data('id');

        swal({
                title: "Are You Sure ?",
                text: "Confirm Removing Package Details..",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, remove it!',
                closeOnConfirm: true,
                closeOnCancel: true

            },
            function(isConfirm) {
                if (isConfirm) {

                    $.ajax({

                        type: 'Post',
                        url: PackageUrl.url_remove,
                        ajaxasync: true,
                        data: { packageId: _packageId },
                        success: function (data) {

                            if (data.success)
                            {

                                swal({
                                        title: "Success",
                                        text: "Package was succesfully remove!",
                                        type: "success"
                                    },function(){});

                            }
                            else {

                                swal({
                                    title: "Unable to Remove Package " + data.package_name,
                                    text: "Verify package has exsiting Bookings!",
                                    type: "warning"
                                }, function () { });


                               // swal("Unable to Remove!", "Verify package has exsiting Bookings !", "warning");
                            }

                            setTimeout(function () {

                                window.location.href = PackageUrl.url_index;
                               
                               // $('#spinn-loader').hide();
                            }, 2000);
                          

                           
                        }
                    });
                }


            });


});


$(document).on('click','#package_edit',function(e) {

    e.preventDefault();
    e.stopPropagation();

    var _packageId = $(this).data('id');

    window.location.href = PackageUrl.url_modifypackage.replace("pId", _packageId);

});


$(document).on('click', '#btn_modifyMainPackage', function (e) {

    e.preventDefault();

    
    var form = $(this).closest('form');


    swal({
            title: "Are You Sure ?",
            text: "Confirm Packages Modification..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Update it!',
            closeOnConfirm: true, closeOnCancel: true

        },
        function (isConfirm) {
            if (isConfirm) {

                var formUrl = $('#modify_packageMain').attr('action');

                //var form = $('[id*=new_packageForm]');

                //console.log(form);

                form.removeData("validator");
                form.removeData("unobtrusiveValidation");

                $.validator.unobtrusive.parse(form);
                form.validate();

                if (form.valid()) {

                    //console.log('valid form');

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

                                setTimeout(function () {

                                    $('#packageform').load(data.url);

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
                        $errorSpan.html("<span style='color:red'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }

                //  showlastrecord(menulist);
            }
        });





});


$(document).on('click','#btn_updatePackagedetails',function(e) {

    e.preventDefault();

    var form = $(this).closest('form');


    swal({
            title: "Are You Sure ?",
            text: "Confirm Updating Packages..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Update it!',
            closeOnConfirm: true, closeOnCancel: true

        },
        function (isConfirm) {
            if (isConfirm) {

                var formUrl = $('#form_modifypackagebody').attr('action');

                //var form = $('[id*=new_packageForm]');

                //console.log(form);

                form.removeData("validator");
                form.removeData("unobtrusiveValidation");

                $.validator.unobtrusive.parse(form);
                form.validate();

                if (form.valid()) {

                    //console.log('valid form');

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

                                setTimeout(function () {

                                    window.location.href = PackageUrl.url_details.replace("pId", data.packageId);

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
                        $errorSpan.html("<span style='color:red'>" + value.message + "</span>");
                        $errorSpan.show();
                    });
                }

                //  showlastrecord(menulist);
            }
        });

});

$(document).on('click','#btn_skip',function(e) {

    e.preventDefault();
    e.stopPropagation();

    $('#packageform').load(PackageUrl.url_modifyPackageBody.replace("pId", $(this).data('id')));


});


$(document).on('click', '#btnCancelmodifyPackage', function (e) {

    e.preventDefault();

    swal({
            title: "Are You Sure ?",
            text: "Confirm Cancel Package Modification ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Cancel!',
            closeOnConfirm: true, closeOnCancel: true

        },
        function (isConfirm) {
            if (isConfirm) {

                window.location.href = PackageUrl.url_index;

            }

        });

});


$(document).on('click', '#btn_removelocPackage',function(e)
{
    e.preventDefault();
    var p_areaId = $(this).data('id');


    swal({
            title: "Are You Sure ?",
            text: "Confirm removing this location..",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, remove it!',
            closeOnConfirm: true,
            closeOnCancel: true

        },
        function(isConfirm) {

            if (isConfirm) {

                $.ajax({

                    type: 'Post',
                    url: PackageUrl.url_removePackageAreaCoverage,
                    ajaxasync: true,
                    data: { pAreaNo: p_areaId },
                    success: function (data) {

                        if (data.success) {

                            swal({
                                title: "Success",
                                text: "Package was succesfully remove!",
                                type: "success"
                            }, function () { });

                        }

                        setTimeout(function () {

                            $('#packagelocationTable').load(data.url);

                            // $('#spinn-loader').hide();
                        }, 500);



                    }
                });
            }


        });

});



$(document).on('click','#addpackageLocation', function (e) {


    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    //delete existing modal
    $('.modal').replaceWith('');

    //get modal


    $.ajax({
        //'/Packages/AddPackageCoverage

        url: PackageUrl.url_addPackageAreaCoverage,
        type: 'Get',
        contentType: "application/json; charset=utf-8",
        data: { packageId: $(this).data('id') },
        cache: false,
        success: function (data) {

            var modal = $(data);
            $('body').append(modal);

            reparseform_packagelocation();

            $('#select2Area').select2({

                dropdownParent: $('#modal-addpackagecoverage'),

                placeholder: "Search Location",
                minimumInputLength: 2,  // minimumInputLength for sending ajax request to server
                width: 'resolve',
                ajax: {
                    //url: '/Packages/GetAreas/',
                    url: PackageUrl.url_searchPackageLocation,
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        var queryParameters = {
                            query: params.term
                        }

                        return queryParameters;
                    },
                    processResults: function (data) {
                        return { results: data.areaList }
                    }
                },

                formatResult: formatRepo,
                formatSelection: formatRepoSelection

            }).on("select2:select", function (e) {
                var selectedId = "";
                var selected_element = $(e.currentTarget);

                selectedId = selected_element.val();

                $('#hidden_AreaLocationId').val(selectedId);
            });

            modal.modal({
                backdrop: 'static',
                keyboard: false
            });

        }
    });

});


function reparseform_packagelocation() {
    $("#packagelocationform").removeData("validator");
    $("#packagelocationform").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse('#packagelocationform');
    //console.log('sadasd');

}