var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/PurchaseOrders/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "purchaseOrderName", "width": "5%" },
            { "data": "branch.branchName", "width": "4%" },
            { "data": "vendor.vendorName", "width": "4%" },
            { "data": "orderDate", "width": "4%" },
            { "data": "deliveryDate", "width": "4%" },
            { "data": "currency.currencyName", "width": "4%" },
            { "data": "purchaseType.purchaseTypeName", "width": "5%" },
            { "data": "purchaseInvoiceName", "width": "5%" },

            {
                "data": "purchaseOrderId",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/PurchaseOrders/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:60px;' >
                                <i class='far fa-edit'></i> 
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:60px;' onclick=Delete('/PurchaseOrders/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> 
                            </a>
                               <a href="/PurchaseOrders/Invoice/${data}" title="invoice" class='btn btn-primary text-white' style='cursor:pointer; width:60px;' >
                                <i class='fas fa-print'></i> 
                            </a>
                            </div>
                        `;
                }, "width": "30%"
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