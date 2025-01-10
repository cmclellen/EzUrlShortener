/* eslint-disable react-refresh/only-export-components */
import {
  cloneElement,
  createContext,
  ReactElement,
  ReactNode,
  useContext,
  useState,
} from "react";
import { createPortal } from "react-dom";

interface ModalContextType {
  openName: string;
  closeModal: () => void;
  openModal: (openName: string) => void;
}

const ModalContext = createContext<ModalContextType>({
  openName: "",
  closeModal: () => {},
  openModal: () => {},
});

export function useModal() {
  const context = useContext(ModalContext);
  if (context === null)
    throw new Error("ModalContext was used outside of ModalProvider");
  return context;
}

interface ModalProps {
  children: ReactNode;
}

function Modal({ children }: ModalProps) {
  const [openName, setOpenName] = useState("");
  const closeModal = () => setOpenName("");
  const openModal = (openName: string) => setOpenName(openName);

  return (
    <ModalContext.Provider value={{ openName, closeModal, openModal }}>
      {children}
    </ModalContext.Provider>
  );
}

interface OpenProps {
  children: ReactElement;
  opensWindowName: string;
}

function Open({ children, opensWindowName }: OpenProps) {
  const { openModal } = useContext(ModalContext);
  return cloneElement(children, { onClick: () => openModal(opensWindowName) });
}

interface WindowProps {
  children: ReactNode;
  name: string;
}

function Window({ children, name }: WindowProps) {
  const { openName } = useContext(ModalContext);
  if (openName !== name) return null;
  return createPortal(
    <div
      className="relative z-10"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div
        className="fixed inset-0 bg-gray-500/75 transition-opacity"
        aria-hidden="true"
      ></div>

      <div className="fixed inset-0 z-10 w-screen overflow-y-auto">
        <div className="flex min-h-full items-center justify-center p-0 text-center">
          <div className="relative my-8 w-full max-w-lg transform overflow-hidden rounded-lg bg-white p-4 text-left shadow-xl transition-all">
            {children}
          </div>
        </div>
      </div>
    </div>,
    document.body,
  );
}

Modal.Open = Open;
Modal.Window = Window;

export default Modal;
