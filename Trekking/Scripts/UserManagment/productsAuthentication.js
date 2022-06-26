$(document).ready(function(){
    let trekkinguser = JSON.parse(localStorage.getItem('trekkinguser'));
    if(trekkinguser?.Role==="admin"){
        let modalButton = document.createElement('button')
        modalButton.classList.add('btn')
        modalButton.classList.add('btn-outline-secondary')
        modalButton.classList.add('float-start')
        modalButton.setAttribute('data-bs-toggle', 'modal')
        modalButton.setAttribute('data-bs-target', '#modalSignin')
        modalButton.textContent = "Create Product"
        $("#createbuttonsec").append(modalButton)
        $("#createbuttonsec").show()
        //results-section
        //     < button
        // className = "btn btn-primary"
        // data - bs - toggle = "modal"
        // data - bs - target = "#modalSignin" > MODAL < /button>
    }
    else{
    }
})