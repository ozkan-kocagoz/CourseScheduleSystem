
const API_URL = "http://localhost:5001/api/auth";
async function handleLogin(event) {
    event.preventDefault();
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    try {
        const response = await fetch(`${API_URL}/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        const data = await response.json();

        if (response.ok) {
            localStorage.setItem("user", JSON.stringify(data));
            window.location.href = "dashboard.html";
        } else {
            document.getElementById("error-message").innerText = data.message || "Giriş başarısız.";
            document.getElementById("error-message").style.display = "block";
        }
    } catch (error) {
        console.error(error);
        document.getElementById("error-message").innerText = "Sunucu hatası!";
        document.getElementById("error-message").style.display = "block";
    }
}