using UnityEngine;

namespace CodeBase.Grid {
    public class DraggableItem : MonoBehaviour {
        private Vector3 offset; // Смещение для корректного захвата предмета
        private bool isDragging = false;
        private GridManager gridManager;
        private Vector2Int originalGridPosition; // Изначальная позиция в сетке

        void Start() {
            gridManager = FindObjectOfType<GridManager>();
        }

        void OnMouseDown() {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();

            // Запоминаем начальную позицию в сетке
            
            originalGridPosition = gridManager.WorldToGridPosition(transform.position);
        }

        void OnMouseDrag() {
            if (isDragging) {
                // Перемещаем объект вместе с мышкой
                transform.position = GetMouseWorldPosition() + offset;
            }
        }

        void OnMouseUp() {
            isDragging = false;

            // Конвертируем текущую позицию в координаты сетки
            Vector2Int newGridPosition = gridManager.WorldToGridPosition(transform.position);

            // Проверяем, свободна ли ячейка
            if (gridManager.IsCellEmpty(newGridPosition.x, newGridPosition.y)) {
                // Обновляем позицию предмета и сетки
                Vector3 snappedPosition = gridManager.GridToWorldPosition(newGridPosition.x, newGridPosition.y);
                transform.position = snappedPosition;

                // Перемещаем объект из старой ячейки в новую
                gridManager.RemoveItemFromCell(originalGridPosition.x, originalGridPosition.y);
                gridManager.PlaceItemInCell(newGridPosition.x, newGridPosition.y, gameObject);
            }
            else {
                // Если ячейка занята, возвращаем предмет на прежнюю позицию
                Vector3 originalPosition = gridManager.GridToWorldPosition(originalGridPosition.x, originalGridPosition.y);
                transform.position = originalPosition;
            }
        }

        // Получаем позицию мыши в мировых координатах
        private Vector3 GetMouseWorldPosition() {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}
