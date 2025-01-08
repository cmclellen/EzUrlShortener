import { Outlet } from "react-router-dom";

function AppLayout() {
  return (
    <>
      <div className="font-poppins relative">
        <div
          className="fixed inset-0 bg-gray-500/75 transition-opacity"
          aria-hidden="true"
        ></div>

        <div className="fixed inset-0">
          <div className="container mx-auto flex h-dvh items-center justify-center">
            <div className="w-full rounded-lg bg-white p-10 shadow-xl shadow-stone-900/15">
              <Outlet />
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default AppLayout;
