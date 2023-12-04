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
            { "data": "branch.branchName", "width": "5%" },
            { "data": "vendor.vendorName", "width": "5%" },
            { "data": "orderDate", "width": "5%" },
            { "data": "deliveryDate", "width": "5%" },
            { "data": "currency.currencyName", "width": "5%" },
            { "data": "purchaseType.purchaseTypeName", "width": "5%" },
            { "data": "freight", "width": "5%" },
       
            {
                "data": "purchaseOrderId",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/PurchaseOrders/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class='far fa-edit'></i> Edit
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;' onclick=Delete('/PurchaseOrders/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> Delete
                            </a>
                               <a href="/PurchaseOrders/Invoice/${data}" class='btn btn-primary text-white' style='cursor:pointer; width:100px;' >
                                <i class='fas fa-print'></i> Print
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

function
    Delete(url) {
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
