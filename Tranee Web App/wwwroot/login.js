var tokenKey = "accessToken";
// при нажатии на кнопку отправки формы идет запрос к /login для получения токена
document.getElementById("submitLogin").addEventListener("click", async e => {
    e.preventDefault();
    // отправляет запрос и получаем ответ
    const response = await fetch("/Login/Login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: document.getElementById("name").value,
            password: document.getElementById("password").value
        })
    });
    // если запрос прошел нормально
    if (response.ok === true) {
        // получаем данные
        const data = await response.json();
        // изменяем содержимое и видимость блоков на странице
        document.getElementById("userName").innerText = data.name;
        document.getElementById("userInfo").style.display = "block";
        document.getElementById("loginForm").style.display = "none";
        // сохраняем в хранилище sessionStorage токен доступа
        sessionStorage.setItem(tokenKey, data.access_token);
    }
    else  // если произошла ошибка, получаем код статуса
        console.log("Status: ", response.status);
});

// кнопка для обращения по пути "/data" для получения данных
document.getElementById("getData").addEventListener("click", async e => {
    e.preventDefault();
    // получаем токен из sessionStorage
    const token = sessionStorage.getItem(tokenKey);
    // отправляем запрос к "/data
    const response = await fetch("/Home", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token  // передача токена в заголовке
        }
    });

    if (response.ok === true) {
        window.open(responce.json());
        // const data = await response.json();
        // window.open("/Home" + data, "_self")
    }
    else
        console.log("Status: ", response.status);
});

// условный выход - просто удаляем токен и меняем видимость блоков
document.getElementById("logOut").addEventListener("click", e => {

    e.preventDefault();
    document.getElementById("userName").innerText = "";
    document.getElementById("userInfo").style.display = "none";
    document.getElementById("loginForm").style.display = "block";
    sessionStorage.removeItem(tokenKey);
});