var searchInput = document.getElementById("searchInput")
var searchButton = document.getElementById("searchButton")
var searchDiv= document.getElementById('searchDiv')
searchDiv.innerText = "Introduceti codul unui trip!"
searchDiv.style.display = 'none'
searchDiv.style.color = 'red'
searchInput.value = ''

searchButton.onclick = () => {
    if (searchInput.value == '') {
        searchDiv.style.display = 'block'

    }
    else {
        $.ajax({
            type: "post",
            url: '/Trip/SearchTrip',
            datatype: "json",
            data: { code: searchInput.value }
        })
            .fail(() => {
                searchDiv.innerText = "Nu exista un trip cu acest cod!"
                searchDiv.style.display = 'block'
            })
            .done((id) => {
                window.location.href = '../Trip/TripDetails/' + id;
            })
    }
}