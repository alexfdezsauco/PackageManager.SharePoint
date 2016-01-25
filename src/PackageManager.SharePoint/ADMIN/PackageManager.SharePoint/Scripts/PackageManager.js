function newPackageSource() {
    var options = {
        url: "/_admin/PackageManager.SharePoint/NewPackageSource.aspx",
        allowMaximize: false,
        showClose: true,
        title: "New Package Source",
        width: 400,
        height: 100,
        dialogReturnValueCallback: function(dialogResult, comment) {
            window.location.href = window.location.href;
        }
    };

    SP.SOD.execute("sp.ui.dialog.js", "SP.UI.ModalDialog.showModalDialog", options);
}