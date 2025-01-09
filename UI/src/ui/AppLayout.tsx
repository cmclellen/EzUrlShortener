import { Outlet } from "react-router-dom";

function AppLayout() {
  return (
    <>
      <div className="relative font-poppins">
        <div
          className="fixed inset-0 bg-gray-500/75 transition-opacity"
          aria-hidden="true"
        ></div>

        <div className="fixed inset-0">
          <div className="mx-auto flex h-dvh max-w-md items-center justify-center">
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
