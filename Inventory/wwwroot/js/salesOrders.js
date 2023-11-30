var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/SalesOrders/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "branch.branchName", "width": "5%" },
            { "data": "customer.customerName", "width": "5%" },
            { "data": "orderDate", "width": "5%" },
            { "data": "deliveryDate", "width": "5%" },
            { "data": "currency.currencyName", "width": "5%" },
            { "data": "salesType.salesTypeName", "width": "5%" },
            { "data": "freight", "width": "5%" },

            {
                "data": "salesOrderId",
                "render": function (data) {

                    return `<div class="text-center"> 
                                <!-- Edit Button -->
                            <a href="/SalesOrders/Upsert/${data}" class='btn btn-success text-white' title="Edit" style='cursor:pointer; width:60px;' >
                                <i class='far fa-edit'></i> 
                            </a>
                            &nbsp;
                            <!-- Delete Button -->
                            <a class='btn btn-danger text-white' title="Delete" style='cursor:pointer; width:60px;' onclick=Delete('/SalesOrders/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> 
                            </a>
                            &nbsp;
                            <!-- Print Button -->
                            <a href="/SalesOrders/Invoice/${data}" class='btn btn-primary text-white' title="Print" style='cursor:pointer; width:60px;' >
                               <i class='fas fa-print'></i> 
                            </a>
                            </div>`;
                }, "width": "20%"
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
