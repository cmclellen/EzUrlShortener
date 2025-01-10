import { TfiAlert } from "react-icons/tfi";
import Button from "./Button";
import { useModal } from "./Modal";

interface ConfirmDeleteProps {
  resourceName: string;
  disabled?: boolean;
  onDelete: () => void;
}

function ConfirmDelete({
  resourceName,
  disabled = false,
  onDelete,
}: ConfirmDeleteProps) {
  const { closeModal } = useModal();
  return (
    <>
      <div className="flex items-center justify-start space-x-2 font-semibold">
        <TfiAlert className="text-xl text-red-800" />
        <span className="text uppercase tracking-wide">
          Delete {resourceName}
        </span>
      </div>
      <div>Are you sure you want to delete this {resourceName}?</div>
      <div className="flex justify-end gap-1">
        <Button onClick={closeModal} disabled={disabled}>
          Cancel
        </Button>
        <Button
          disabled={disabled}
          className="bg-orange-500 text-white"
          onClick={onDelete}
        >
          Delete
        </Button>
      </div>
    </>
  );
}

export default ConfirmDelete;
