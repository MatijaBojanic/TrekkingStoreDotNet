$(document).ready(function(){
    $("#btn-login").click(function(ev) {
        let email = $("#email").val()
        let password = $("#password").val()
        
        if(!email || !password){
            alert("Please insert all data")
        }
        else{
            $.ajax({
                type:"POST",
                url:"http://localhost:5000/api/user/login",
                data:{ email:email, password:password},
                success: function(data){
                    if(data){
                        localStorage.setItem('trekkinguser', JSON.stringify(data))
                        let url = '/'
                        window.location.href = url;
                    }
                    else{
                        alert('fail')
                    }
                }
            })
        }
    })
})