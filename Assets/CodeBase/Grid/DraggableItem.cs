using UnityEngine;

namespace CodeBase.Grid {
    public class DraggableItem : MonoBehaviour
    {
        private Vector3 offset;  // Смещение для корректного захвата предмета
        private bool isDragging = false;
        private GridManager gridManager;

        void Start()
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
        }

        void OnMouseDrag()
        {
            if (isDragging)
            {
                Vector2Int oldGridPosition = gridManager.WorldToGridPosition(transform.position);
                gridManager.RemoveItemFromCell(oldGridPosition.x, oldGridPosition.y);
                transform.position = GetMouseWorldPosition() + offset;
            }
        }

        void OnMouseUp()
        {
            isDragging = false;

            // Конвертируем позицию в координаты сетки
            Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
            // Проверяем, свободна ли ячейка
            if (gridManager.IsCellEmpty(gridPosition.x, gridPosition.y))
            {
                Debug.Log(gridManager.IsCellEmpty(gridPosition.x, gridPosition.y) + "gridManager.IsCellEmpty(gridPosition.x, gridPosition.y) ");
                // Обновляем позицию предмета и сетки
            
                Vector3 snappedPosition = gridManager.GridToWorldPosition(gridPosition.x, gridPosition.y);
                transform.position = snappedPosition;
            
           
            
                gridManager.PlaceItemInCell(gridPosition.x, gridPosition.y, gameObject);
            }
            else
            {
                // Если ячейка занята, возвращаем предмет на прежнюю позицию
                Vector3 originalPosition = gridManager.GridToWorldPosition(1, 1);
                transform.position = originalPosition;
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}