import { NavLink, Outlet } from "react-router-dom";

const navItems = [
  { text: "Shorten URL", to: "/add-url" },
  { text: "List URLs", to: "/list-urls" },
];

function AppLayout() {
  return (
    <>
      <div className="relative font-poppins">
        <div
          className="fixed inset-0 bg-gray-500/75 transition-opacity"
          aria-hidden="true"
        ></div>

        <div className="fixed inset-0">
          <div className="flex w-auto flex-grow space-x-3 bg-blue-700 p-4 text-sm text-white">
            {navItems.map((item) => (
              <NavLink
                to={item.to}
                className={({ isActive, isPending }) =>
                  isPending
                    ? "pending"
                    : isActive
                      ? "font-semibold"
                      : "hover:text-blue-100"
                }
              >
                {item.text}
              </NavLink>
            ))}
          </div>

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