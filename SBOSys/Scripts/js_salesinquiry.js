$(document).ready(function() {

    //Custom date validation overide for date formats
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD-MMM-YYYY", true).isValid();
    }

    //init datetimepcker
    $('#start_date').datetimepicker(
        {

            format: "DD-MMM-YYYY"

        });

    //init datetimepcker
    $('#end_date').datetimepicker(
        {

            format: "DD-MMM-YYYY"

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


    $('#btnFilterschedule').on('click',
        function(e) {
            e.preventDefault();


            var dateStart = $('#start_date').val();
            var dateEnd = $('#end_date').val();


            if (dateStart === "" && dateEnd === "") {

                swal("Pls. enter date value!", "Unable to process request", "error");

            } else {

           

                $('#spinn-loader').show();

                $.ajax({
                    type: 'Get',
                    url: salessummarycheduleUrl.url_load_loadingSscheduleDataTable,
                    //data: { datefrom: dateStart, dateTo: dateEnd },
                    contentType: 'application/html;charset=utf8',
                    datatype: 'html',
                    cache: false,
                    success: function (result) {

                        $('#filteredRecord').html(result);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal("Error on retrieving record!", "Please try again", "error");
                    }


                }).done(function () {

                    loadScheduleDateFilter(dateStart, dateEnd);

                    $('#spinn-loader').hide();
                });

            }

        });//---- end button filter Click event


    function loadScheduleDateFilter(dateStart, dateEnd) {
        
        if ($.fn.dataTable.isDataTable('#tbl_salessummary')) {

            $('#tbl_salessummary').DataTable().destroy();
            $('tbl_salessummary tbody').empty();

        }

        var tablesales = $('#tbl_salessummary').DataTable({
           
            "serverSide": false,
            "processing": true,
            "language": {

                'processing': '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only"> Loading data Pls. wait....</span>'
            },

            "dom": "<'row'<'col-sm-6'B><'col-sm-6'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-6'i><'col-sm-6'p>>"
            ,
            "ajax":
            {
                "url": salessummarycheduleUrl.url_loadsalessummarylist,
                "type": "Get",
                "data": {startDate:dateStart,endDate: dateEnd },
                "datatype": "json"
            }
            ,
            "columnDefs":
            [

                {
                    'autowidth': true, 'targets': 0,
                    "data": "dateTrans",
                    "type": "date",
                    "render": function (d) {
                        return moment(d).format("MMM-DD-YYYY hh:mm: A");
                    }

                },
                {
                    'autowidth': true, 'targets': 1,
                    "data": "accountname"
                },


                {
                    'autowidth': true, 'targets': 2,
                    "data": "reference"
                },

                {
                    'autowidth': true, 'targets': 3,
                    "data": "particular"
                },
                {
                    'autowidth': true,
                    'targets': 4,
                    "orderable": false,
                    "className": "text-right",
                    "data": "CashSales",
                    render: $.fn.dataTable.render.number(",", ".", 2)

                },
                {
                    'autowidth': true,
                    'targets': 5,
                    "orderable": false,
                    "className": "text-right",
                    "data": "OnAccount",
                    render: $.fn.dataTable.render.number(",", ".", 2)

                }

            ]
            
            ,
            buttons:
            [
               
                {
                    extend: 'pdfHtml5',
                    text: '<i class="fa fa-print fa-sm fa-fw"></i>',
                    className: 'btn btn-primary btn-sm btnPrintPayment',
                    titleAttr: 'Print to PDF',
                    download: 'open',
                    orientation:'portrait',
                    exportOptions: {
                        columns: ':visible'
                    },
                    pageSize: 'LETTER',
                    footer: true,
                    customize:function(doc) {
                        doc.styles.title.fontSize = 12;
                        doc.defaultStyle.fontSize = 8;
                        doc.styles.tableHeader.fontSize = 9;
                    }
                  

                }
            ]
        });
    }
});