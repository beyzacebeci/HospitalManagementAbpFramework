$(function () {
    var createModal = new abp.ModalManager(abp.appPath + 'Hospitals/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Hospitals/EditModal');

    var hospitalService = hospitalManagement.hospitals.hospital;

    var dataTable = $('#HospitalsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(hospitalService.getList),
            columnDefs: [
                {
                    title: 'Actions',
                    rowAction: {
                        items:
                            [
                                {
                                    text: 'Edit',
                                    visible: abp.auth.isGranted('HospitalManagement.Hospitals.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: 'Delete',
                                    visible: abp.auth.isGranted('HospitalManagement.Hospitals.Delete'), 
                                    confirmMessage: function (data) {
                                        return "Are you sure to delete the hospital '" + data.record.name + "'?";
                                    },
                                    action: function (data) {
                                        hospitalService
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info("Successfully deleted!");
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: 'Name',
                    data: "name"
                },
                {
                    title: 'Departments',
                    data: "departmentNames",
                    orderable: false,
                    render: function (data) {
                        return data.join(", ");
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewHospitalButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});