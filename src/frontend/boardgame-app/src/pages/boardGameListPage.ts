import {
  createBoardGame,
  deleteBoardGame,
  getBoardGames,
} from '../api/boardGameApi';
import type { BoardGame } from '../models/boardGame';
import type { CreateBoardGameRequest } from '../models/createBoardGameRequest';

export async function renderBoardGameListPage(
  container: HTMLDivElement,
  formMessage = '',
  formMessageType: 'success' | 'error' = 'success',
): Promise<void> {
  container.innerHTML = `
    <main class="page">
      <section class="page-header">
        <h1>Board Game Catalog</h1>
        <p>Browse the games currently stored in the backend.</p>
      </section>

      <section class="create-form-card">
        <h2>Add a new board game</h2>
        <form id="create-board-game-form" class="board-game-form">
          <div class="form-grid">
            <label class="form-field">
              <span>Title</span>
              <input name="title" type="text" required />
            </label>

            <label class="form-field">
              <span>Publisher</span>
              <input name="publisher" type="text" required />
            </label>

            <label class="form-field">
              <span>Min players</span>
              <input name="minPlayers" type="number" min="1" required />
            </label>

            <label class="form-field">
              <span>Max players</span>
              <input name="maxPlayers" type="number" min="1" required />
            </label>

            <label class="form-field">
              <span>Estimated play time</span>
              <input name="estimatedPlayTime" type="number" min="1" required />
            </label>

            <label class="form-field">
              <span>Category</span>
              <input name="category" type="text" required />
            </label>
          </div>

          <div id="form-message" class="form-message ${getFormMessageClass(formMessageType)}">
            ${formMessage}
          </div>

          <button type="submit" class="submit-button">Create board game</button>
        </form>
      </section>

      <section class="board-game-section">
        <p id="page-status" class="status-message">Loading board games...</p>
        <div id="board-game-list" class="board-game-list"></div>
      </section>
    </main>
  `;

  const formElement = container.querySelector<HTMLFormElement>('#create-board-game-form');
  const formMessageElement = container.querySelector<HTMLDivElement>('#form-message');
  const statusElement = container.querySelector<HTMLParagraphElement>('#page-status');
  const listElement = container.querySelector<HTMLDivElement>('#board-game-list');

  if (!formElement || !formMessageElement || !statusElement || !listElement) {
    return;
  }

  if (!formMessage) {
    formMessageElement.style.display = 'none';
  }

  formElement.addEventListener('submit', async (event) => {
    event.preventDefault();

    formMessageElement.style.display = 'none';
    formMessageElement.textContent = '';
    formMessageElement.className = 'form-message';

    try {
      const request = getCreateBoardGameRequest(formElement);

      await createBoardGame(request);
      formElement.reset();

      await renderBoardGameListPage(
        container,
        'Board game created successfully.',
        'success',
      );
    } catch (error) {
      const message =
        error instanceof Error
          ? error.message
          : 'Something went wrong while creating the board game.';

      formMessageElement.textContent = message;
      formMessageElement.style.display = 'block';
      formMessageElement.classList.add('form-message--error');
    }
  });

  try {
    const boardGames = await getBoardGames();

    if (boardGames.length === 0) {
      statusElement.textContent = 'No board games found.';
      return;
    }

    statusElement.remove();
    renderBoardGameCards(listElement, boardGames);
    attachDeleteButtonHandlers(container);
  } catch (error) {
    const message =
      error instanceof Error
        ? error.message
        : 'Something went wrong while loading board games.';

    statusElement.textContent = message;
    statusElement.classList.add('status-message--error');
  }
}

function getCreateBoardGameRequest(
  formElement: HTMLFormElement,
): CreateBoardGameRequest {
  const formData = new FormData(formElement);

  return {
    title: String(formData.get('title') ?? ''),
    publisher: String(formData.get('publisher') ?? ''),
    minPlayers: Number(formData.get('minPlayers')),
    maxPlayers: Number(formData.get('maxPlayers')),
    estimatedPlayTime: Number(formData.get('estimatedPlayTime')),
    category: String(formData.get('category') ?? ''),
  };
}

function attachDeleteButtonHandlers(container: HTMLDivElement): void {
  const deleteButtons =
    container.querySelectorAll<HTMLButtonElement>('.delete-button');

  deleteButtons.forEach((deleteButton) => {
    deleteButton.addEventListener('click', async () => {
      const boardGameId = deleteButton.dataset.id;

      if (!boardGameId) {
        return;
      }

      const isConfirmed = window.confirm(
        'Are you sure you want to delete this board game?',
      );

      if (!isConfirmed) {
        return;
      }

      try {
        await deleteBoardGame(boardGameId);
        await renderBoardGameListPage(
          container,
          'Board game deleted successfully.',
          'success',
        );
      } catch (error) {
        const message =
          error instanceof Error
            ? error.message
            : 'Something went wrong while deleting the board game.';

        await renderBoardGameListPage(container, message, 'error');
      }
    });
  });
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
          <div class="card-actions">
            <button
              type="button"
              class="delete-button"
              data-id="${boardGame.id}"
            >
              Delete
            </button>
          </div>
        </article>
      `,
    )
    .join('');
}

function getFormMessageClass(messageType: 'success' | 'error'): string {
  return messageType === 'error'
    ? 'form-message--error'
    : 'form-message--success';
}
