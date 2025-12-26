import { marked } from "marked";
import { createResource } from "solid-js";

export function Marked(props: { children: string }) {
  const [content] = createResource(
    () => props.children,
    (data) => {
      return data ? marked.parse(data, { async: true }) : undefined;
    }
  );

  return <div class="markdown-content" innerHTML={content()} />;
}
