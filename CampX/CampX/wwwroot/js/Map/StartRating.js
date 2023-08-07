function RatingStarSystem() {
    var starInputs = document.querySelectorAll(".star-rating input[type='radio']")
    let ratingValue = 0
    starInputs.forEach((input) => {

        input.onclick = () => {
            var selectedIndex = Array.from(starInputs).indexOf(input);
            for (var i = 0; i <= selectedIndex; i++) {
                starInputs[i].nextElementSibling.style.color = "gold";

            }
            for (var i = selectedIndex + 1; i < starInputs.length; i++) {
                starInputs[i].nextElementSibling.style.color = "#ccc";
            }
            ratingValue = input.value
        }
    })
    return ratingValue
}
