/* eslint-disable @typescript-eslint/no-explicit-any */
import { useForm } from "react-hook-form";
import PageLayout from "../ui/PageLayout";
import FormError from "../ui/FormError";
import useShortenUrl from "../features/shortenUrl/useShortenUrl";

function isValidHttpUrl(urlText: string) {
  try {
    const newUrl = new URL(urlText);
    return newUrl.protocol === "http:" || newUrl.protocol === "https:";
  } catch {
    return false;
  }
}

function AddUrl() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  function onSubmit(data: any) {
    shortenUrl(data.url);
  }

  const { shortenUrl } = useShortenUrl();

  return (
    <PageLayout title="Add URL">
      <form
        className="flex flex-col space-y-4"
        onSubmit={handleSubmit(onSubmit)}
      >
        <label>
          <label htmlFor="url" className="mb-2 block text-sm font-medium">
            URL
          </label>
          <input
            type="url"
            id="url"
            placeholder="https://example.com"
            className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-blue-500 focus:ring-blue-500"
            {...register("url", {
              required: "URL is required",
              validate: (value) => isValidHttpUrl(value) || "Invalid URL",
            })}
          />
          {errors?.url?.message && (
            <FormError message={errors.url.message}></FormError>
          )}
        </label>
        <button
          type="submit"
          className="w-full rounded-lg bg-blue-700 px-5 py-2.5 text-sm font-medium text-white transition duration-300 ease-in-out hover:bg-blue-600 focus:outline-none focus:ring-4 focus:ring-blue-100"
        >
          Add
        </button>
      </form>
    </PageLayout>
  );
}

export default AddUrl;
