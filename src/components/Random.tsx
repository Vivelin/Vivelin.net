import { children, JSX } from "solid-js";

type RandomProps = { children: JSX.Element };

export function Random(props: RandomProps) {
  const chs = children(() => props.children);
  const possiblities = chs() as unknown as
    | PossibilityProps
    | PossibilityProps[];

  const items = Array.isArray(possiblities) ? possiblities : [possiblities];
  const i = Math.floor(Math.random() * items.length);

  const item = items[i];
  return item.children;
}

type PossibilityProps = {
  children: JSX.Element;
};

export function Possibility(props: PossibilityProps) {
  return props as unknown as JSX.Element;
}
