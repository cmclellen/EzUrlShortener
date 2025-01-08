import { ReactNode } from "react";

interface PageLayoutProps {
  title: string;
  children: ReactNode;
}

function PageLayout({ title, children }: PageLayoutProps) {
  return (
    <>
      <h1 className="text-center text-xl font-semibold">{title}</h1>
      <div className="mt-4">{children}</div>
    </>
  );
}

export default PageLayout;
