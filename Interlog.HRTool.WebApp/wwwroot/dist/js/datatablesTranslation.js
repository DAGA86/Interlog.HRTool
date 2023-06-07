$(function () {

    function fetchJSONFile() {
        return new Promise((resolve, reject) => {
            fetch(translationJsonFilePath)
                .then(response => response.json())
                .then(data => resolve(data));
        });
    }

    fetchJSONFile()
        .then(jsonData => {
            $("#example1").DataTable({
                "responsive": true,
                language: jsonData,
                "lengthChange": false,
                "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
        });
});