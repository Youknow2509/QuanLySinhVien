// Call the dataTables jQuery plugin
$(document).ready(function() {
  $('#dataTable').DataTable(
    {
        language: {
            url: '/sb-admin/json/data_tables_vi.json',
        },
    }
  );
});
