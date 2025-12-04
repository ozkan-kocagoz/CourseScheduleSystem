const user = JSON.parse(localStorage.getItem("user"));

if (!user) {
    window.location.href = "login.html";
} else {
    document.getElementById("user-name").innerText = `Merhaba, ${user.fullName}`;
}

function logout() {
    localStorage.removeItem("user");
    window.location.href = "login.html";
}
const API_BASE_URL = "http://localhost:5001/api"; 



async function loadDepartments() {
    const list = document.getElementById("course-list");

    const countBox = document.getElementById("dept-count"); 

    list.innerHTML = "<li style='text-align:center;'>Veriler Yükleniyor... <i class='fas fa-spinner fa-spin'></i></li>";

    try {
        const response = await fetch(`${API_BASE_URL}/departments`);
        if (!response.ok) throw new Error("Veri çekilemedi!");

        const departments = await response.json();
        list.innerHTML = "";

        countBox.innerText = departments.length; 
        // -------------------------------------------

        departments.forEach(dept => {
            const li = document.createElement("li");
            li.innerHTML = `
                <span>${dept.name}</span>
                <span class="badge" style="background:#007bff; color:white; padding:2px 8px; border-radius:4px; font-size:12px;">${dept.code}</span>
            `;
            li.style.display = "flex";
            li.style.justifyContent = "space-between";
            li.style.padding = "10px";
            li.style.borderBottom = "1px solid #eee";
            list.appendChild(li);
        });
    } catch (error) {
        console.error("Hata:", error);
        list.innerHTML = "<li style='color:red;'>Veriler alınamadı. Backend çalışıyor mu?</li>";
        countBox.innerText = "0";
    }
}
    setTimeout(() => {
        list.innerHTML = `
            <li>✅ Yazılım Mühendisliği Oryantasyonu</li>
            <li>✅ Veri Yapıları ve Algoritmalar</li>
            <li>✅ Web Programlama</li>
        `;
    }, 1000);

async function handleRegister(event) {
    event.preventDefault();
    const msgLabel = document.getElementById("register-message");
    msgLabel.style.color = "blue";
    msgLabel.innerText = "İşlem yapılıyor, lütfen bekleyin...";

    const fullName = document.getElementById("new-name").value;
    const email = document.getElementById("new-email").value;
    const role = document.getElementById("new-role").value;

    try {
        const response = await fetch(`${API_BASE_URL}/auth/register`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ 
                fullName: fullName, 
                email: email, 
                role: role, 
                departmentId: null 
            })
        });

        const data = await response.json();

        if (response.ok) {
            msgLabel.style.color = "green";
            msgLabel.innerText = "✅ " + data.message;
            // Formu temizle
            document.getElementById("new-name").value = "";
            document.getElementById("new-email").value = "";
        } else {
            msgLabel.style.color = "red";
            msgLabel.innerText = "❌ " + (data.message || "Hata oluştu");
        }
    } catch (error) {
        msgLabel.style.color = "red";
        msgLabel.innerText = "Sunucu hatası!";
        console.error(error);
    }
    
}

/**
 * @param {string} sectionId - Gösterilecek bölümün idsi (home veya users)
 */
function showSection(sectionId) {
    document.getElementById('home-section').style.display = 'none';
    document.getElementById('users-section').style.display = 'none';
    document.getElementById(sectionId + '-section').style.display = 'block';
    document.querySelectorAll('.sidebar li').forEach(li => {
        li.classList.remove('active');
    });
    document.getElementById('menu-' + sectionId).classList.add('active');
}


document.addEventListener("DOMContentLoaded", function() {
    updateDashboardStats();
});

async function updateDashboardStats() {
    const userCountBox = document.getElementById("user-count");

    try {
        const response = await fetch(`${API_BASE_URL}/users`);
        
        if (response.ok) {
            const users = await response.json();
            userCountBox.innerText = users.length;
        }
    } catch (error) {
        console.error("Kullanıcı sayısı alınamadı:", error);
        userCountBox.innerText = "?";
    }
    if(typeof loadDepartments === "function") {
        loadDepartments();
    }
}