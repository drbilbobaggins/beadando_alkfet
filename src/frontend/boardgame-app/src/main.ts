import './style.css';

const app = document.querySelector<HTMLDivElement>('#app');

if (app) {
  app.innerHTML = `
    <div class="app">
      <h1>Board Game Catalog</h1>
      <p>The frontend project is running.</p>
    </div>
  `;
}