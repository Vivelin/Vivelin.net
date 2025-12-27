import { useMatch } from "@solidjs/router";
import { JSX } from "solid-js";
import "./Nav.css";

export function Nav(props: {}) {
  return (
    <nav>
      <ul>
        <NavLink href="/">Index</NavLink>
        <NavLink href="/about">About</NavLink>
        <NavLink href="/test">Test</NavLink>
        <NavLink href="/cats">Cats</NavLink>
      </ul>
    </nav>
  );
}

function NavLink(props: { href: string; children: JSX.Element }) {
  const match = useMatch(() => props.href);

  return (
    <li>
      <a href={props.href} classList={{ active: Boolean(match()) }}>
        {props.children}
      </a>
    </li>
  );
}
