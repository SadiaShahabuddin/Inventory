var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Branches/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "branchName", "width": "5%" },
            { "data": "currencyId", "width": "5%" },
            { "data": "address", "width": "5%" },
            { "data": "city", "width": "5%" },
            { "data": "state", "width": "5%" },
            { "data": "zipCode", "width": "5%" },
            { "data": "phone", "width": "5%" },
            { "data": "email", "width": "5%" },
            { "data": "contactPerson", "width": "5%" },
            { "data": "description", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/Branches/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100%;' >
                                <i class='far fa-edit'></i> Edit
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:100%;' onclick=Delete('/Branches/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> Delete
                            </a></div>
                        `;
                }, "width": "100%"
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
