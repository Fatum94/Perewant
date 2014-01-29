var getRole  = {
    getUserId: function() {
        var cookie = Ext.util.Cookies.get('auth_test'),
            id = cookie.split('id')[1];
        return id;
    },
    doRequest: function() {
        var view = this;
        Ext.Ajax.request({
            url: '/Home/GetUserRole',
            method: 'GET',
            params: {
                id: view.getUserId()
            },
            success: function(response, res) {
                view.userRole = JSON.parse(response.responseText).response;
                runApp();
            }
        });
    },
    getUserRole: function() {
        this.doRequest();
        return this.userRole;
    }
}