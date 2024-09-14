using UnityEngine;

namespace CodeBase.Grid {
    public class GridManager : MonoBehaviour {
        public int rows = 5; // Количество строк в сетке
        public int cols = 4; // Количество столбцов в сетке
        public float cellSize = 1f; // Размер одной ячейки
        public GameObject gridItemPrefab; // Префаб предмета

        private GridCell[,] grid;

        void Start() {
            InitializeGrid(); // Инициализация сетки

            // Пример размещения предметов на сетке
           
            Debug.Log("Cell " + 2 + ", " + 3 + " is now: " + grid[2, 3].isEmpty);
        }

        public void CreateItemOnGrid(int y, int x) {
            if (IsCellEmpty(y, x)) {
                Vector3 position = GridToWorldPosition(y, x);
                GameObject item = Instantiate(gridItemPrefab, position, Quaternion.identity);
                PlaceItemInCell(y, x, item);
            }
        }
        // Метод для инициализации сетки

        public void InitializeGrid() {
            grid = new GridCell[rows, cols];

            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) {
                    grid[y, x] = new GridCell();
                    grid[y, x].isEmpty = true;
                }
            }
        }

        // Метод для конвертации позиции из мировых координат в координаты сетки

        public Vector2Int WorldToGridPosition(Vector3 worldPosition) {
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int y = Mathf.FloorToInt(worldPosition.y / cellSize);
            return new Vector2Int(x, y);
        }

        // Метод для конвертации координат сетки в мировые координаты

        public Vector3 GridToWorldPosition(int x, int y) {
            // Вычисляем центр ячейки
            float worldY = y * cellSize + cellSize / 2;
            float worldX = x * cellSize + cellSize / 2;

            // Возвращаем позицию в мире с нужной Z координатой (например, на уровне пола)
            return new Vector3(worldX, worldY, 0);
        }

        // Метод для проверки, пуста ли ячейка

        public bool IsCellEmpty(int y, int x) {
            if (y >= 0 && x >= 0 && y < cols && x < rows) {
                return grid[y, x].isEmpty;
            }

            return false;
        }

        // Метод для размещения предмета в ячейке

        public void PlaceItemInCell(int y, int x, GameObject item) {
            if (!IsCellEmpty(y, x)) {
                Debug.LogWarning("Attempted to place item in a non-empty cell: " + y + ", " + x);
                return; // Если ячейка не пуста, просто выходим из метода
            }

            // Размещаем предмет
            grid[y, x].PlaceItem(item);


            for (int i = 0; i < rows; i++) {
                for (int e = 0; e < cols; e++) {
                    Debug.Log(grid[i, e].isEmpty + " IS EMPTY ");
                }
            }
        }

        // Метод для удаления предмета из ячейки

        public void RemoveItemFromCell(int y, int x) {
            if (y >= 0 && y < rows && x >= 0 && x < cols)
            {
                // Удаляем элемент из ячейки, не проверяя её заполненность
                grid[y, x].RemoveItem();
            }
        }


        void OnDrawGizmos() {
            Gizmos.color = Color.black;

            // Отрисовка вертикальных линий
            for (int x = 0; x <= cols; x++) {
                Vector3 start = new Vector3(x * cellSize, 0, 0); // Начало на оси Y = 0
                Vector3 end = new Vector3(x * cellSize, rows * cellSize, 0); // Конец на оси Y = количество строк
                Gizmos.DrawLine(start, end); // Рисуем вертикальные линии
            }

            // Отрисовка горизонтальных линий
            for (int y = 0; y <= rows; y++) {
                Vector3 start = new Vector3(0, y * cellSize, 0); // Начало на оси X = 0
                Vector3 end = new Vector3(cols * cellSize, y * cellSize, 0); // Конец на оси X = количество столбцов
                Gizmos.DrawLine(start, end); // Рисуем горизонтальные линии
            }

            // Дополнительно можно отрисовать позиции, куда будут ставиться кубы
            Gizmos.color = Color.red;
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) {
                    // Получаем центр каждой ячейки
                    Vector3 position = GridToWorldPosition(x, y);

                    // Отрисовка маленького куба в центре каждой ячейки
                    Gizmos.DrawCube(position, Vector3.one * (cellSize * 0.1f));
                }
            }
        }
    }
}

