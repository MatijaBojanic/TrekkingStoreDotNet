$(document).ready(function(){
    $("#logoutAnchor").click(function(ev){
        localStorage.removeItem("trekkinguser")
    })
    
    let trekkinguser = localStorage.getItem('trekkinguser');
    if(!trekkinguser){
        $('#loginbtn').show()
        $('#signupbtn').show()
        $('#logoutbtn').hide()
    }
    else {
        $('#loginbtn').hide()
        $('#signupbtn').hide()
        $('#logoutbtn').show()
    }
})

