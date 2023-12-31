var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Warehouses/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "warehouseName", "width": "25%" },
            { "data": "description", "width": "25%" },
            { "data": "branch.branchName", "width": "25%" },
            {
                "data": "warehouseId",
                "render": function (data) {

                    return `<div class="text-center">
                            <a href="/Warehouses/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class='far fa-edit'></i> Edit
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;' onclick=Delete('/Warehouses/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> Delete
                            </a></div>
                        `;
                }, "width": "30%"
            }


        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
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