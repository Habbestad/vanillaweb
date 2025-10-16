
let incrementBtn = document
    .getElementById("increment")
    .addEventListener("click", () => {
        let count = document.getElementById("count")
        count.innerText = (Number(count.innerText) + 1).toString()
    })


