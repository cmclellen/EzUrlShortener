import { Outlet } from "react-router-dom";

function AppLayout() {
  return (
    <>
      {/* <nav>
        <div className="bg-blue-700 p-4 text-white">
          <div className="container mx-auto flex items-center justify-between">
            <h1 className="text-xl font-semibold">Shorten URL</h1>
          </div>
        </div>
      </nav> */}

      <div className="relative font-poppins">
        <div
          className="fixed inset-0 border border-red-500 bg-gray-500/75 transition-opacity"
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
