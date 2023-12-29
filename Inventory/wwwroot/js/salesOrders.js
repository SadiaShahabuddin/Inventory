var dataTable;

$(document).ready(function () {
    loadDataTable();
});

// Make an Ajax call
    //$.ajax({
    //    url: "/Warehouses/GetAll",
    //    type: "GET",
    //    dataType: "json",
    //    success: function (data) {
    //        // Log the response in the console
    //        console.log(data);
    //    },
    //    error: function (error) {
    //        console.error("Error loading data: " + error);
    //    }
    //});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/SalesOrders/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "salesOrderName", "width": "5%" },
            { "data": "branch.branchName", "width": "4%" },
            { "data": "customer.customerName", "width": "4%" },
            { "data": "orderDate", "width": "4%" },
            { "data": "deliveryDate", "width": "4%" },
            { "data": "currency.currencyName", "width": "4%" },
            { "data": "salesType.salesTypeName", "width": "5%" },
            { "data": "salesInvoiceName", "width": "5%" },

            {
                "data": "salesOrderId",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/SalesOrders/Upsert/${data}" title="Edit" class='btn btn-success text-white' style='cursor:pointer; width:60px;' >
                                <i class='far fa-edit'></i> 
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' title="Delete" style='cursor:pointer; width:60px;' onclick=Delete('/SalesOrders/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> 
                            </a>
                            &nbsp;
                            <a href="/SalesOrders/Invoice/${data}" title="Invoice Print" class='btn btn-primary text-white' style='cursor:pointer; width:60px;' >
                                invoice</i>
                            </a>
                            </div>
                        `;
                }, "width": "20%"
            }


        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure want to Delete?",
        text: "You will not be able to restore the file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();

                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}