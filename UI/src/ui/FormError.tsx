interface FormErrorProps {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  message: any;
}

function FormError({ message }: FormErrorProps) {
  return <span className="text-sm font-semibold text-red-900">{message}</span>;
}

export default FormError;
