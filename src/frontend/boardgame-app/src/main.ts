import './style.css';
import { renderBoardGameListPage } from './pages/boardGameListPage';

const app = document.querySelector<HTMLDivElement>('#app');

if (app) {
  renderBoardGameListPage(app);
}
