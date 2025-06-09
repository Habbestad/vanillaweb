
const button = document.getElementById("try-js-btn")

button.addEventListener("click", () => {
    console.log("Button clicked")
    let body = document.getElementById("body").style.backgroundColor
    if(body === "red") {
        document.getElementById("body").style.backgroundColor = "green"
    }
    else {
        document.getElementById("body").style.backgroundColor = "red"
    }
})

