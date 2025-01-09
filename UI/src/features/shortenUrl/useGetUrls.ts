import { useQuery } from "@tanstack/react-query";
import { getUrls as getUrlsApi } from "../../services/apiShortenUrl";

export default function useGetUrls() {
  const { data: urls, isLoading: isLoadingUrls } = useQuery({
    queryKey: ["urls"],
    queryFn: getUrlsApi,
  });

  return { urls, isLoadingUrls };
}
