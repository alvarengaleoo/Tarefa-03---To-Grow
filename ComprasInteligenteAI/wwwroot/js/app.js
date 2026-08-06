const btnAnalisar = document.getElementById("btnAnalisar");
const spinner = document.getElementById("spinner");
const iconeBotao = document.getElementById("iconeBotao");

btnAnalisar.addEventListener("click", analisarCompra);

async function analisarCompra() {

    const descricao = document.getElementById("descricao").value.trim();
    const valor = Number(document.getElementById("valor").value);
    const departamento = document.getElementById("departamento").value;

    if (descricao === "") {
        alert("Informe a descrição da compra.");
        return;
    }

    btnAnalisar.disabled = true;

    spinner.classList.remove("d-none");
    iconeBotao.classList.add("d-none");

    try {

        const response = await fetch("/api/compras/analisar", {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({

                descricao: descricao,
                valorEstimado: valor,
                departamento: departamento

            })

        });

        if (!response.ok) {
            throw new Error("Erro ao consultar a IA.");
        }

        const resultado = await response.json();

        document.getElementById("categoria").textContent = resultado.categoria;
        document.getElementById("prioridade").textContent = resultado.prioridade;
        document.getElementById("risco").textContent = resultado.risco;
        document.getElementById("justificativa").textContent = resultado.justificativa;
        document.getElementById("sugestao").textContent = resultado.sugestao;

        document.getElementById("resultado").classList.remove("d-none");

        window.scrollTo({
            top: document.body.scrollHeight,
            behavior: "smooth"
        });

    }
    catch (erro) {

        alert("Não foi possível analisar a solicitação.");

        console.error(erro);

    }
    finally {

        btnAnalisar.disabled = false;

        spinner.classList.add("d-none");
        iconeBotao.classList.remove("d-none");

    }

}