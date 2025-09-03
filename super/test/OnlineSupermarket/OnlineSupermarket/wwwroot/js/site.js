document.addEventListener("DOMContentLoaded", () => {
    setTimeout(() => {
        document.querySelector(".animate-title").style.opacity = 1;
        document.querySelector(".animate-title").style.transform = "translateY(0)";
    }, 300);

    setTimeout(() => {
        document.querySelector(".animate-subtitle").style.opacity = 1;
        document.querySelector(".animate-subtitle").style.transform = "translateY(0)";
    }, 600);

    setTimeout(() => {
        document.querySelectorAll(".animate-btn").forEach(btn => {
            btn.style.opacity = 1;
            btn.style.transform = "scale(1)";
        });
    }, 900);
});
