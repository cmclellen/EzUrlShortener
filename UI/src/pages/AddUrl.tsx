/* eslint-disable @typescript-eslint/no-explicit-any */
import { useForm } from "react-hook-form";
import PageLayout from "../ui/PageLayout";
import FormError from "../ui/FormError";
import useShortenUrl from "../features/shortenUrl/useShortenUrl";

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
          <label>URL</label>
          <input
            type="url"
            placeholder="https://example.com"
            className="rounded border border-gray-300 px-2 py-1"
            {...register("url", {
              required: "URL is required",
            })}
          />
          {errors?.url?.message && (
            <FormError message={errors.url.message}></FormError>
          )}
        </label>
        <button
          type="submit"
          className="rounded bg-blue-500 px-4 py-2 text-white"
        >
          Add
        </button>
      </form>
    </PageLayout>
  );
}

export default AddUrl;
