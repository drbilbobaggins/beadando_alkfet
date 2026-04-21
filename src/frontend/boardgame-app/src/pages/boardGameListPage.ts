import { getBoardGames } from '../api/boardGameApi';
import type { BoardGame } from '../models/boardGame';

export async function renderBoardGameListPage(
  container: HTMLDivElement,
): Promise<void> {
  container.innerHTML = `
    <main class="page">
      <section class="page-header">
        <h1>Board Game Catalog</h1>
        <p>Browse the games currently stored in the backend.</p>
      </section>

      <section class="board-game-section">
        <p id="page-status" class="status-message">Loading board games...</p>
        <div id="board-game-list" class="board-game-list"></div>
      </section>
    </main>
  `;

  const statusElement = container.querySelector<HTMLParagraphElement>('#page-status');
  const listElement = container.querySelector<HTMLDivElement>('#board-game-list');

  if (!statusElement || !listElement) {
    return;
  }

  try {
    const boardGames = await getBoardGames();

    if (boardGames.length === 0) {
      statusElement.textContent = 'No board games found.';
      return;
    }

    statusElement.remove();
    renderBoardGameCards(listElement, boardGames);
  } catch (error) {
    const message =
      error instanceof Error
        ? error.message
        : 'Something went wrong while loading board games.';

    statusElement.textContent = message;
    statusElement.classList.add('status-message--error');
  }
}

function renderBoardGameCards(
  listElement: HTMLDivElement,
  boardGames: BoardGame[],
): void {
  listElement.innerHTML = boardGames
    .map(
      (boardGame) => `
        <article class="board-game-card">
          <h2>${boardGame.title}</h2>
          <p><strong>Publisher:</strong> ${boardGame.publisher}</p>
          <p><strong>Players:</strong> ${boardGame.minPlayers} - ${boardGame.maxPlayers}</p>
          <p><strong>Play time:</strong> ${boardGame.estimatedPlayTime} minutes</p>
          <p><strong>Category:</strong> ${boardGame.category}</p>
        </article>
      `,
    )
    .join('');
}
