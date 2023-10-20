<script type="module">
    import {Client} from "https://cdn.passwordless.dev/dist/1.1.0/esm/passwordless.min.mjs"

        window.passwordlessDev = async () => {
            const p = new Client({
        apiKey: "yourAPIKey"
            });

    // Allow the user to specify a username or alias.
    const alias = "yourname@passwordless.dev";

    // Generate a verification token for the user.
    const {token, error} = await p.signinWithAlias(alias);
    return token;
        }
</script>