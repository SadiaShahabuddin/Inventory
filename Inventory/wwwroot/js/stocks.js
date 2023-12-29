var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Products/GetStock",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": null, "width": "5%",
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { "data": "productName", "width": "20%" },
            { "data": "totalPurchase", "width": "25%" },
            { "data": "totalSales", "width": "25%" },
            { "data": "currentStock", "width": "25%" },
            { "data": "branchName", "width": "25%" },
            {
                "data": "currentStock", "width": "25%",
                "render": function (data) {
                    if (data < 3) {
                        return '<span class="text-danger">Low Stock:</span>';
                    } else {
                        return '';
                    }
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}