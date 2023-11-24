import axios from "axios";
const cus_classs = {
  namespaced: true,
  state: {
    cus_class: null,
    status_chose: 1,
  },
  mutations: {
    set_value: (state, value) => {
      state.cus_class = value;
    },
  },
  getters: {
    getbyid: (state) => (idcus, idclass) => {
      return state.cus_class.filter(
        (e) => e.customer_id == idcus && e.class_id == idclass
      )[0];
    },
    getbyidcus: (state) => (idcus) => {
      return state.cus_class.filter((e) => e.customer_id == idcus);
    },
    getbyidclass: (state) => (idclass) => {
      return state.cus_class.filter(
        (e) => e.class_id == idclass && e.status_pay == true
      );
    },
    getbyidclass_payfalse: (state) => (idclass) => {
      return state.cus_class.filter(
        (e) => e.class_id == idclass && e.status_pay == false
      );
    },
    getbyidclass_notpay: (state) => (idclass) => {
      const check_date = (value) => {
        const date = new Date(value);
        date.setDate(Number(date.getDate()) + 2);
        if (date.getTime() <= new Date().getTime()) {
          return false;
        }
        return true;
      };
      return state.cus_class.filter(
        (e) =>
          e.class_id == idclass &&
          e.status_pay == false &&
          check_date(e.date_create) == false
      );
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/customer_class");
      commit("set_value", data.data);
    },

    async update({ commit }, datass) {
      await axios.put(
        "http://localhost:3000/customer_class/" + datass.id,
        datass
      );
      const data = await axios.get("http://localhost:3000/customer_class");
      commit("set_value", data.data);
    },

    async add_cus({ commit }, data) {
      await axios.post("http://localhost:3000/customer_class", data);
      const datas = await axios.get("http://localhost:3000/customer_class");
      commit("set_value", datas.data);
    },
    async delete({ commit }, id) {
      console.log(commit);
      await axios
        .delete("http://localhost:3000/customer_class/" + id)
        .catch(() => {
          console.log("Mihequifh");
        });
    },
    async deleteid({ commit }, id) {
      console.log(commit);
      await axios
        .delete("http://localhost:3000/customer_class/" + id)
        .catch(() => {
          console.log("Mihequifh");
        });
      const data = await axios.get("http://localhost:3000/customer_class");
      commit("set_value", data.data);
    },
  },
};
export default cus_classs;
