﻿var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/directions/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "cosmicSpot.name", "width": "25%" },
            { "data": "name", "width": "20%" },
            { "data": "distance", "width": "15%" },
            { "data": "elevation", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/directions/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete("/directions/Delete/+${data}")>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                    </div>`;
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
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnconfirm: true
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

