import { ReactNode } from "react";
import clsx from "clsx";
interface ButtonProps {
  children: ReactNode;
  className?: string;
  disabled?: boolean;
  onClick: () => void;
}
function Button({
  children,
  disabled = false,
  className = "",
  onClick,
}: ButtonProps) {
  return (
    <button
      disabled={disabled}
      onClick={onClick}
      className={clsx(
        `rounded-lg bg-gray-300 px-4 py-2 font-semibold tracking-wider text-gray-900`,
        className,
      )}
    >
      {children}
    </button>
  );
}

export default Button;
